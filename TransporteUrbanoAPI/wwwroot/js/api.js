async function request(method, path, body) {
  const token = localStorage.getItem("token");
  let res;
  try {
    res = await fetch(API_URL + path, {
      method,
      headers: {
        "Content-Type": "application/json",
        ...(token ? { Authorization: "Bearer " + token } : {})
      },
      body: body !== undefined ? JSON.stringify(body) : undefined
    });
  } catch {
    throw new Error("No se pudo conectar con la API. Verificá que esté corriendo.");
  }

  if (res.status === 401 && !path.startsWith("/auth")) {
    logout();
    throw new Error("La sesión venció");
  }

  if (res.status === 403) throw new Error("No tenés permisos para realizar esta acción");

  const text = await res.text();
  let data = null;
  if (text) {
    try { data = JSON.parse(text); } catch { data = text; }
  }
  if (!res.ok) throw new Error(mensajeDeError(data, res.status));
  return data;
}

function mensajeDeError(d, status) {
  if (d && d.message) return d.message;                                 // reglas de negocio
  if (d && d.errors) return Object.values(d.errors).flat().join(" ");   // validaciones de los DTOs
  if (typeof d === "string" && d) return d;
  return "Error " + status;
}

const api = {
  get: p => request("GET", p),
  post: (p, b) => request("POST", p, b),
  put: (p, b) => request("PUT", p, b),
  del: p => request("DELETE", p)
};