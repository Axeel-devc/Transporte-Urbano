requireLogin();   // cualquier usuario con sesion puede verla

document.getElementById("usuario").textContent = localStorage.getItem("usuario");
document.getElementById("salirBtn").onclick = logout;

// Solo el admin ve el acceso al panel
if (rolActual() === "Admin") document.getElementById("panelLink").hidden = false;

// Pestañas
const tabs = document.querySelectorAll("[data-tab]");
function mostrar(nombre) {
    tabs.forEach(t => {
        const activa = t.dataset.tab === nombre;
        t.classList.toggle("active", activa);
        t.setAttribute("aria-selected", activa);
    });
    document.querySelectorAll("[data-panel]").forEach(p => p.hidden = p.dataset.panel !== nombre);
    history.replaceState(null, "", "#" + nombre);
    window.scrollTo(0, 0);
}
tabs.forEach(t => t.onclick = () => mostrar(t.dataset.tab));
mostrar(location.hash === "#equipo" ? "equipo" : "proyecto");

document.querySelectorAll("[data-icon]").forEach(el => el.insertAdjacentHTML("afterbegin", icon(el.dataset.icon)));