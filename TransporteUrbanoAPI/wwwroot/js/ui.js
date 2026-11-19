const ICONS = {
  bus: '<path d="M8 6v6"/><path d="M15 6v6"/><path d="M2 12h19.6"/><path d="M18 18h3s.5-1.7.8-2.8c.1-.4.2-.8.2-1.2 0-.4-.1-.8-.2-1.2l-1.4-5C20.1 6.8 19.1 6 18 6H4a2 2 0 0 0-2 2v10h3"/><circle cx="7" cy="18" r="2"/><path d="M9 18h5"/><circle cx="16" cy="18" r="2"/>',
  wrench: '<path d="M14.7 6.3a1 1 0 0 0 0 1.4l1.6 1.6a1 1 0 0 0 1.4 0l3.77-3.77a6 6 0 0 1-7.94 7.94l-6.91 6.91a2.12 2.12 0 0 1-3-3l6.91-6.91a6 6 0 0 1 7.94-7.94l-3.76 3.76z"/>',
  dashboard: '<rect width="7" height="9" x="3" y="3" rx="1"/><rect width="7" height="5" x="14" y="3" rx="1"/><rect width="7" height="9" x="14" y="12" rx="1"/><rect width="7" height="5" x="3" y="16" rx="1"/>',
  info: '<circle cx="12" cy="12" r="10"/><path d="M12 16v-4"/><path d="M12 8h.01"/>',
  logout: '<path d="M9 21H5a2 2 0 0 1-2-2V5a2 2 0 0 1 2-2h4"/><polyline points="16 17 21 12 16 7"/><line x1="21" x2="9" y1="12" y2="12"/>',
  menu: '<line x1="4" x2="20" y1="6" y2="6"/><line x1="4" x2="20" y1="12" y2="12"/><line x1="4" x2="20" y1="18" y2="18"/>',
  plus: '<path d="M5 12h14"/><path d="M12 5v14"/>',
  edit: '<path d="M17 3a2.85 2.83 0 1 1 4 4L7.5 20.5 2 22l1.5-5.5Z"/>',
  trash: '<path d="M3 6h18"/><path d="M19 6v14c0 1-1 2-2 2H7c-1 0-2-1-2-2V6"/><path d="M8 6V4c0-1 1-2 2-2h4c1 0 2 1 2 2v2"/>',
  chev: '<path d="m6 9 6 6 6-6"/>',
  chart: '<path d="M3 3v18h18"/><path d="M18 17V9"/><path d="M13 17V5"/><path d="M8 17v-3"/>'
};

function icon(name, extra = "") {
  return `<svg class="i ${extra}" viewBox="0 0 24 24" aria-hidden="true">${ICONS[name]}</svg>`;
}

// Escapa texto antes de usar innerHTML
function esc(s) {
  return String(s ?? "").replace(/[&<>"']/g, c => ({ "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;" }[c]));
}

function money(n) {
  return new Intl.NumberFormat("es-AR", { style: "currency", currency: "ARS", maximumFractionDigits: 0 }).format(n ?? 0);
}

function fechaAR(iso) {            // "2026-10-08" -> "08/10/2026"
  if (!iso) return "";
  const [y, m, d] = iso.split("-");
  return `${d}/${m}/${y}`;
}

function toast(msg, tipo = "ok") {
  const el = document.createElement("div");
  el.className = "toast" + (tipo === "error" ? " error" : "");
  el.textContent = msg;
  document.getElementById("toasts").appendChild(el);
  setTimeout(() => el.remove(), 3500);
}

// Dialogo de confirmacion: devuelve true / false
function confirmar(titulo, texto, botonOk = "Eliminar") {
  return new Promise(resolve => {
    const dlg = document.createElement("dialog");
    dlg.innerHTML = `
      <div class="dialog-head"><h3>${esc(titulo)}</h3></div>
      <div class="dialog-body">${esc(texto)}</div>
      <div class="dialog-foot">
        <button class="btn btn-white" data-r="0">Cancelar</button>
        <button class="btn btn-danger" data-r="1">${esc(botonOk)}</button>
      </div>`;
    dlg.addEventListener("click", e => {
      const r = e.target.closest("[data-r]");
      if (r) { dlg.close(); resolve(r.dataset.r === "1"); }
    });
    dlg.addEventListener("close", () => { dlg.remove(); resolve(false); });
    document.body.appendChild(dlg);
    dlg.showModal();
  });
}

function countUp(el, hasta, formato = v => Math.round(v).toLocaleString("es-AR"), ms = 900) {
  const ini = performance.now();
  function paso(t) {
    const p = Math.min((t - ini) / ms, 1);
    el.textContent = formato(hasta * (1 - Math.pow(1 - p, 3)));
    if (p < 1) requestAnimationFrame(paso);
  }
  requestAnimationFrame(paso);
}

function claseEstado(estado) {
  if (estado === "En servicio") return "badge-ok";
  if (estado === "En mantenimiento") return "badge-warn";
  if (estado === "Fuera de servicio") return "badge-bad";
  return "";
}