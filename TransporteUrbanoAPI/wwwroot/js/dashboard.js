function renderStats(d) {
  const enServicio = d.porEstado.find(e => e.nombre === "En servicio")?.cantidad ?? 0;
  countUp(document.getElementById("sTotal"), d.totalColectivos);
  countUp(document.getElementById("sServicio"), enServicio);
  countUp(document.getElementById("sSinMant"), d.sinMantenimiento.length);
  countUp(document.getElementById("sCosto"), d.costoUltimos6Meses, money);
}

function renderBars(id, items) {
  const cont = document.getElementById(id);
  if (!items.length) { cont.innerHTML = '<div class="empty">Sin datos</div>'; return; }
  const max = Math.max(...items.map(i => i.cantidad));
  cont.innerHTML = items.map(i => `
    <div class="bar-row">
      <span>${esc(i.nombre)}</span>
      <div class="bar-track"><div class="bar-fill" data-w="${(i.cantidad / max) * 100}"></div></div>
      <span class="n">${i.cantidad}</span>
    </div>`).join("");
  animar(cont);
}

function renderCols(meses) {
  const cont = document.getElementById("cols");
  const max = Math.max(...meses.map(m => m.total), 1);
  const compacto = new Intl.NumberFormat("es-AR", { notation: "compact", maximumFractionDigits: 1 });
  cont.innerHTML = meses.map(m => `
    <div class="col" title="${esc(money(m.total))}">
      <span class="col-val">${m.total > 0 ? compacto.format(m.total) : "–"}</span>
      <div class="col-area"><div class="col-bar" data-h="${(m.total / max) * 100}"></div></div>
      <span>${esc(m.mes)}</span>
    </div>`).join("");
  animar(cont);
}

function renderSinMant(lista) {
  const cont = document.getElementById("sinMant");
  if (!lista.length) {
    cont.innerHTML = '<div class="empty">Todos los colectivos tuvieron mantenimiento en los últimos 6 meses.</div>';
    return;
  }
  cont.innerHTML = `<table>
    <thead><tr><th>Coche</th><th>Patente</th><th>Línea</th></tr></thead>
    <tbody>${lista.map(c => `<tr><td>${c.nroCoche}</td><td>${esc(c.patente)}</td><td>${esc(c.numeroLinea)}</td></tr>`).join("")}</tbody>
  </table>`;
}

// Las barras arrancan en 0 y crecen hasta su valor (transicion CSS)
function animar(cont) {
  requestAnimationFrame(() => requestAnimationFrame(() => {
    cont.querySelectorAll(".bar-fill").forEach(b => b.style.width = b.dataset.w + "%");
    cont.querySelectorAll(".col-bar").forEach(b => b.style.height = b.dataset.h + "%");
  }));
}

(async function () {
  document.getElementById("saludo").textContent = "Hola, " + localStorage.getItem("usuario");
  try {
    const d = await api.get("/dashboard");
    renderStats(d);
    renderBars("barsEstado", d.porEstado);
    renderBars("barsLinea", d.porLinea);
    renderCols(d.costoPorMes);
    renderSinMant(d.sinMantenimiento);
  } catch (e) {
    toast(e.message, "error");
  }
})();