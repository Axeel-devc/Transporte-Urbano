if (sesionValida()) location.href = rutaInicial();

const form = document.getElementById("registroForm");
const error = document.getElementById("error");
const boton = document.getElementById("btnRegistro");

form.addEventListener("submit", async e => {
    e.preventDefault();
    error.textContent = "";

    const password = document.getElementById("password").value;
    if (password !== document.getElementById("password2").value) {
        error.textContent = "Las contraseñas no coinciden";
        return;
    }

    boton.disabled = true;
    boton.textContent = "Creando cuenta...";
    try {
        const r = await api.post("/auth/registro", {
            nombreUsuario: document.getElementById("usuario").value,
            email: document.getElementById("email").value,
            password
        });
        guardarSesion(r);
        location.href = rutaInicial();   // un usuario nuevo es Visitante: va a la presentacion
    } catch (err) {
        error.textContent = err.message;
        boton.disabled = false;
        boton.textContent = "Crear cuenta";
    }
});