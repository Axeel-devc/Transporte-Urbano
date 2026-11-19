// Dibuja el menu lateral y la barra superior de las paginas internas
requireAdmin();

const MENU = [
  { titulo: "Dashboard", icono: "dashboard", href: "principal.html" },
  { titulo: "Soporte", icono: "bus", items: [{ titulo: "Colectivos", href: "colectivos.html" }] },
  { titulo: "Transacción", icono: "wrench", items: [{ titulo: "Mantenimientos", href: "mantenimientos.html" }] },
  { titulo: "Acerca de", icono: "info", items: [{ titulo: "Integrantes", href: "acerca.html" }] }
];

(function dibujarLayout() {
  const actual = location.pathname.split("/").pop() || "principal.html";

  const links = MENU.map(m => {
    if (!m.items) {
      return `<a class="nav-link ${m.href === actual ? "active" : ""}" href="${m.href}">${icon(m.icono)}${m.titulo}</a>`;
    }
    const abierto = m.items.some(i => i.href === actual) ? "open" : "";
    const subs = m.items.map(i => `<a class="nav-link ${i.href === actual ? "active" : ""}" href="${i.href}">${i.titulo}</a>`).join("");
    return `<details class="nav" ${abierto}><summary>${icon(m.icono)}${m.titulo}${icon("chev", "chev")}</summary><div class="nav-sub">${subs}</div></details>`;
  }).join("");

  document.getElementById("sidebar").innerHTML =
    `<div class="brand"><span class="brand-logo">${icon("bus")}</span>Transporte Urbano</div><nav>${links}</nav>`;

  document.getElementById("topbar").innerHTML = `
    <button class="btn btn-white btn-icon" id="menuBtn" aria-label="Abrir menú">${icon("menu")}</button>
    <span class="spacer"></span>
    <span class="user">${esc(localStorage.getItem("usuario"))}</span>
    <button class="btn btn-white btn-sm" id="salirBtn">${icon("logout")}Salir</button>`;

  document.getElementById("menuBtn").onclick = () => document.getElementById("sidebar").classList.toggle("open");
  document.getElementById("salirBtn").onclick = logout;
})();

// Los elementos con data-icon="nombre" reciben su icono
document.querySelectorAll("[data-icon]").forEach(el => el.insertAdjacentHTML("afterbegin", icon(el.dataset.icon)));