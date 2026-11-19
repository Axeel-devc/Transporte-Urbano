let mantenimientos = [];
let editandoId = null;
const $ = id => document.getElementById(id);

function llenarSelect(id, items, placeholder) {
  const opciones = items.map(i => `<option value="${i.id}">${esc(i.descripcion)}</option>`).join("");
  $(id).innerHTML = (placeholder ? `<option value="">${placeholder}</option>` : "") + opciones;
}

function hoyISO() {
  const h = new Date();
  return `${h.getFullYear()}-${String(h.getMonth() + 1).padStart(2, "0")}-${String(h.getDate()).padStart(2, "0")}`;
}

async function cargar() {
  const coche = $("fCoche").value;
  try {
    mantenimientos = await api.get("/mantenimientos" + (coche ? "?coche=" + coche : ""));
    render();
  } catch (e) {
    toast(e.message, "error");
  }
}

function render() {
  const t = $("buscar").value.trim().toLowerCase();
  const lista = mantenimientos.filter(m => !t || [m.patente, m.tipoMantenimiento, m.descripcion].some(v => (v ?? "").toLowerCase().includes(t)));

  $("contador").textContent = lista.length + (lista.length === 1 ? " mantenimiento" : " mantenimientos");
  $("tbody").innerHTML = lista.length
    ? lista.map(m => `
      <tr>
        <td>${fechaAR(m.fecha)}</td>
        <td><strong>${esc(m.patente)}</strong></td>
        <td>${esc(m.tipoMantenimiento)}</td>
        <td>${esc(m.descripcion) || '<span class="muted">Sin descripción</span>'}</td>
        <td class="num">${m.costo === null ? '<span class="muted">Sin dato</span>' : money(m.costo)}</td>
        <td class="actions">
          <button class="btn btn-ghost btn-icon" data-accion="editar" data-id="${m.codigoMantenimiento}" aria-label="Editar">${icon("edit")}</button>
          <button class="btn btn-ghost btn-icon" data-accion="eliminar" data-id="${m.codigoMantenimiento}" aria-label="Eliminar">${icon("trash")}</button>
        </td>
      </tr>`).join("")
    : '<tr><td colspan="6" class="empty">No hay mantenimientos con esos filtros.</td></tr>';
}

function abrirForm(m) {
  editandoId = m ? m.codigoMantenimiento : null;
  $("dlgTitulo").textContent = m ? "Editar mantenimiento" : "Nuevo mantenimiento";
  $("cocheSel").value = m?.nroCoche ?? $("cocheSel").options[0]?.value;
  $("tipoSel").value = m?.codigoTipoMantenimiento ?? $("tipoSel").options[0]?.value;
  $("fecha").value = m?.fecha ?? hoyISO();
  $("descripcion").value = m?.descripcion ?? "";
  $("costo").value = m?.costo ?? "";
  $("formError").textContent = "";
  $("dlg").showModal();
}

async function guardar(e) {
  e.preventDefault();
  $("formError").textContent = "";
  const body = {
    nroCoche: Number($("cocheSel").value),
    codigoTipoMantenimiento: Number($("tipoSel").value),
    fecha: $("fecha").value,
    descripcion: $("descripcion").value || null,
    costo: $("costo").value === "" ? null : Number($("costo").value)
  };
  $("btnGuardar").disabled = true;
  try {
    if (editandoId === null) await api.post("/mantenimientos", body);
    else await api.put("/mantenimientos/" + editandoId, body);
    $("dlg").close();
    toast(editandoId === null ? "Mantenimiento registrado" : "Cambios guardados");
    await cargar();
  } catch (err) {
    $("formError").textContent = err.message;
  } finally {
    $("btnGuardar").disabled = false;
  }
}

async function eliminar(m) {
  if (!await confirmar("Eliminar mantenimiento", `Se va a eliminar el mantenimiento del ${fechaAR(m.fecha)} del coche ${m.patente}, junto con sus repuestos asociados.`)) return;
  try {
    await api.del("/mantenimientos/" + m.codigoMantenimiento);
    toast("Mantenimiento eliminado");
    await cargar();
  } catch (err) {
    toast(err.message, "error");
  }
}

$("tbody").addEventListener("click", e => {
  const b = e.target.closest("button[data-accion]");
  if (!b) return;
  const m = mantenimientos.find(x => x.codigoMantenimiento === Number(b.dataset.id));
  if (b.dataset.accion === "editar") abrirForm(m);
  else eliminar(m);
});
$("btnNuevo").onclick = () => abrirForm(null);
$("btnCancelar").onclick = () => $("dlg").close();
$("form").addEventListener("submit", guardar);
$("buscar").addEventListener("input", render);
$("fCoche").addEventListener("change", cargar);

(async function init() {
  try {
    const [coches, tipos] = await Promise.all([api.get("/colectivos"), api.get("/tipos-mantenimiento")]);
    const items = coches.map(c => ({ id: c.nroCoche, descripcion: `${c.patente} (línea ${c.numeroLinea})` }));
    llenarSelect("fCoche", items, "Todos los colectivos");
    llenarSelect("cocheSel", items);
    llenarSelect("tipoSel", tipos);
    $("fecha").max = hoyISO();
    await cargar();
  } catch (e) {
    toast(e.message, "error");
  }
})();