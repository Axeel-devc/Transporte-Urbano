let colectivos = [];
let editandoId = null;   // null = alta, numero = modificacion
const $ = id => document.getElementById(id);

function llenarSelect(id, items, placeholder) {
  const opciones = items.map(i => `<option value="${i.id}">${esc(i.descripcion)}</option>`).join("");
  $(id).innerHTML = (placeholder ? `<option value="">${placeholder}</option>` : "") + opciones;
}

async function cargar() {
  const qs = new URLSearchParams();
  if ($("fLinea").value) qs.set("linea", $("fLinea").value);
  if ($("fEstado").value) qs.set("estado", $("fEstado").value);
  const query = qs.toString();
  try {
    colectivos = await api.get("/colectivos" + (query ? "?" + query : ""));
    render();
  } catch (e) {
    toast(e.message, "error");
  }
}

function render() {
  const t = $("buscar").value.trim().toLowerCase();
  const lista = colectivos.filter(c => !t || [c.patente, c.marca, c.modelo].some(v => (v ?? "").toLowerCase().includes(t)));

  $("contador").textContent = lista.length + (lista.length === 1 ? " colectivo" : " colectivos");
  $("tbody").innerHTML = lista.length
    ? lista.map(c => `
      <tr>
        <td>${c.nroCoche}</td>
        <td><strong>${esc(c.patente)}</strong></td>
        <td>${esc(c.marca) || '<span class="muted">-</span>'}</td>
        <td>${esc(c.modelo) || '<span class="muted">-</span>'}</td>
        <td>Línea ${esc(c.numeroLinea)}</td>
        <td><span class="badge ${claseEstado(c.estado)}">${esc(c.estado)}</span></td>
        <td class="actions">
          <button class="btn btn-ghost btn-icon" data-accion="editar" data-id="${c.nroCoche}" aria-label="Editar">${icon("edit")}</button>
          <button class="btn btn-ghost btn-icon" data-accion="eliminar" data-id="${c.nroCoche}" aria-label="Eliminar">${icon("trash")}</button>
        </td>
      </tr>`).join("")
    : '<tr><td colspan="7" class="empty">No hay colectivos con esos filtros.</td></tr>';
}

function abrirForm(c) {
  editandoId = c ? c.nroCoche : null;
  $("dlgTitulo").textContent = c ? `Editar colectivo ${c.nroCoche}` : "Nuevo colectivo";
  $("patente").value = c?.patente ?? "";
  $("marca").value = c?.marca ?? "";
  $("modelo").value = c?.modelo ?? "";
  $("lineaSel").value = c?.codigoLinea ?? $("lineaSel").options[0]?.value;
  $("estadoSel").value = c?.codigoEstado ?? $("estadoSel").options[0]?.value;
  $("formError").textContent = "";
  $("dlg").showModal();
  $("patente").focus();
}

async function guardar(e) {
  e.preventDefault();
  $("formError").textContent = "";
  const body = {
    patente: $("patente").value,
    marca: $("marca").value || null,
    modelo: $("modelo").value || null,
    codigoLinea: Number($("lineaSel").value),
    codigoEstado: Number($("estadoSel").value)
  };
  $("btnGuardar").disabled = true;
  try {
    if (editandoId === null) await api.post("/colectivos", body);
    else await api.put("/colectivos/" + editandoId, body);
    $("dlg").close();
    toast(editandoId === null ? "Colectivo creado" : "Cambios guardados");
    await cargar();
  } catch (err) {
    $("formError").textContent = err.message;
  } finally {
    $("btnGuardar").disabled = false;
  }
}

async function eliminar(c) {
  if (!await confirmar("Eliminar colectivo", `Se va a eliminar el coche ${c.nroCoche} (${c.patente}). Esta acción no se puede deshacer.`)) return;
  try {
    await api.del("/colectivos/" + c.nroCoche);
    toast("Colectivo eliminado");
    await cargar();
  } catch (err) {
    toast(err.message, "error");   // por ejemplo: tiene mantenimientos asociados
  }
}

$("tbody").addEventListener("click", e => {
  const b = e.target.closest("button[data-accion]");
  if (!b) return;
  const c = colectivos.find(x => x.nroCoche === Number(b.dataset.id));
  if (b.dataset.accion === "editar") abrirForm(c);
  else eliminar(c);
});
$("btnNuevo").onclick = () => abrirForm(null);
$("btnCancelar").onclick = () => $("dlg").close();
$("form").addEventListener("submit", guardar);
$("buscar").addEventListener("input", render);
$("fLinea").addEventListener("change", cargar);
$("fEstado").addEventListener("change", cargar);

(async function init() {
  try {
    const [lineas, estados] = await Promise.all([api.get("/lineas"), api.get("/estados-colectivo")]);
    llenarSelect("fLinea", lineas, "Todas las líneas");
    llenarSelect("fEstado", estados, "Todos los estados");
    llenarSelect("lineaSel", lineas);
    llenarSelect("estadoSel", estados);
    await cargar();
  } catch (e) {
    toast(e.message, "error");
  }
})();