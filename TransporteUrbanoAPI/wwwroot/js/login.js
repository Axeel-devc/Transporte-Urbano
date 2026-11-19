if (sesionValida()) location.href = rutaInicial();

const form = document.getElementById("loginForm");
const error = document.getElementById("error");
const boton = document.getElementById("btnLogin");

form.addEventListener("submit", async e => {
  e.preventDefault();
  error.textContent = "";
  boton.disabled = true;
  boton.textContent = "Ingresando...";
  try {
    const r = await api.post("/auth/login", {
      nombreUsuario: document.getElementById("usuario").value,
      password: document.getElementById("password").value
    });
    guardarSesion(r);
    location.href = rutaInicial();
  } catch (err) {
    error.textContent = err.message;
    boton.disabled = false;
    boton.textContent = "Ingresar";
  }
});