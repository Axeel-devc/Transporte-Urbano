function guardarSesion(r) {
    localStorage.setItem("token", r.token);
    localStorage.setItem("usuario", r.usuario);
    localStorage.setItem("rol", r.rol);
}

// El JWT trae la fecha de vencimiento (exp) en el medio, codificada en base64
function sesionValida() {
    const token = localStorage.getItem("token");
    if (!token) return false;
    try {
        const payload = JSON.parse(atob(token.split(".")[1].replace(/-/g, "+").replace(/_/g, "/")));
        return payload.exp * 1000 > Date.now();
    } catch {
        return false;
    }
}

function rolActual() {
    return localStorage.getItem("rol");
}

// El admin va al panel; cualquier otro usuario, a la presentacion
function rutaInicial() {
    return rolActual() === "Admin" ? "principal.html" : "presentacion.html";
}

function logout() {
    ["token", "usuario", "rol"].forEach(k => localStorage.removeItem(k));
    location.href = "login.html";
}

function requireLogin() {
    if (!sesionValida()) logout();
}

// Para las paginas del panel: exige sesion y rol Admin
function requireAdmin() {
    if (!sesionValida()) logout();
    else if (rolActual() !== "Admin") location.href = "presentacion.html";
}