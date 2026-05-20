# Aplicación Maestro-Detalle de Facturación (Persona-Facturas)

Esta es una aplicación móvil y de escritorio desarrollada con **.NET MAUI** utilizando **SQLite** para el almacenamiento local de datos. Está estructurada bajo una relación de uno a muchos (1 Persona -> Varias Facturas) y cuenta con una arquitectura limpia inyectada por dependencias, diseñada para ser sumamente sencilla de usar y entender.

## ¿Cómo funciona la aplicación?

1. **Gestión de Personas (Maestro)**: Al iniciar la aplicación, se presenta la pantalla principal con una lista de todas las personas registradas. Permite buscar personas en tiempo real por su nombre, agregar nuevas personas a través de un formulario emergente, editar sus datos de contacto y eliminarlas de forma definitiva.
2. **Navegación al Detalle**: Cada persona cuenta con un botón verde **`Ver Facturas`**. Al pulsarlo, el sistema navega a una pantalla específica de detalle dedicada exclusivamente a ese cliente.
3. **Gestión de Facturas (Detalle)**: Dentro del detalle de la persona se listan todas las facturas asociadas a ella. Se pueden registrar nuevas facturas ingresando el número de factura, concepto, monto y fecha de emisión. Además, se permite editar y eliminar cada factura de forma independiente.
4. **Integridad de Datos (Borrado en Cascadas)**: Si una persona es eliminada de la base de datos, el sistema se encarga de limpiar automáticamente todas sus facturas asociadas para evitar registros huérfanos.

---

## Galería de Funcionamiento

A continuación, se presentan las secciones correspondientes para documentar visualmente el flujo completo de la aplicación. Puedes añadir tus propias imágenes reemplazando los enlaces de marcador de posición correspondientes.

---

# Persona

### Create (Crear Persona)
*Descripción del flujo:* Presione el botón **`Agregar`** en la parte superior derecha de la pantalla principal o el botón **`Agregar Nueva Persona`** si la lista está vacía, complete el nombre (campo obligatorio), correo electrónico, teléfono y presione **`Guardar`**.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 35 25 PM" src="https://github.com/user-attachments/assets/104f2c2d-ec41-44bb-ac06-35319b05823a" />

---

### Read (Leer / Listar Personas)
*Descripción del flujo:* Muestra la lista de todas las personas registradas en tarjetas individuales. Incluye una barra de búsqueda para filtrar la lista instantáneamente por nombre.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 35 25 PM (1)" src="https://github.com/user-attachments/assets/afce96c5-2490-4399-8c4f-02b4b001a9f5" />


---

### Update (Actualizar Persona)
*Descripción del flujo:* Presione el botón azul **`Editar`** en la tarjeta de la persona que desea modificar. Se abrirá el formulario cargado con los datos actuales; modifique la información requerida y presione **`Guardar`**.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 35 25 PM (2)" src="https://github.com/user-attachments/assets/7e5b243c-cb14-455a-b117-478082b71bd5" />
<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 35 25 PM (3)" src="https://github.com/user-attachments/assets/74879d71-54df-46fc-b105-33c5f05cf83e" />

---

### Delete (Eliminar Persona)
*Descripción del flujo:* Presione el botón rojo **`Eliminar`** en la tarjeta de la persona. Se le mostrará un cuadro de diálogo de confirmación. Al pulsar "Sí", el registro y todas sus facturas asociadas se eliminarán por completo.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 35 24 PM" src="https://github.com/user-attachments/assets/a1bcd57f-ef46-41a2-91cf-8e7899445636" />
<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 35 24 PM (1)" src="https://github.com/user-attachments/assets/56475955-2066-4f30-ab02-e1115ef7e1b3" />

---
---

# Factura

### Create (Crear Factura)
*Descripción del flujo:* Entre al detalle de una persona pulsando **`Ver Facturas`**. Luego, haga clic en el botón verde **`+ Agregar Factura`**, introduzca los datos requeridos (número de factura y monto son obligatorios) y presione **`Guardar`**.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 44 53 PM" src="https://github.com/user-attachments/assets/61cb4605-050b-4719-8195-a95187c5647e" />


---

### Read (Leer / Listar Facturas de Persona)
*Descripción del flujo:* Dentro de la pantalla de detalle de cada persona, se visualiza la lista de sus facturas registradas en un formato claro que detalla el número, concepto, fecha formateada y el monto de cada transacción en color verde.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 44 53 PM (1)" src="https://github.com/user-attachments/assets/6a6cc21f-ab79-4823-b002-378811b547e9" />

---

### Update (Actualizar Factura)
*Descripción del flujo:* En la lista de facturas de una persona, pulse el botón azul **`Editar`** correspondiente a la factura que desea modificar, realice los cambios necesarios y presione **`Guardar`**.

<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 44 53 PM (2)" src="https://github.com/user-attachments/assets/30c192be-f449-497e-8ae9-053e82bf1f5e" />
<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 44 53 PM (3)" src="https://github.com/user-attachments/assets/ef1fc03f-7805-4ae9-b98b-7b0c848d6a9c" />


---

### Delete (Eliminar Factura)
*Descripción del flujo:* Pulse el botón rojo **`Eliminar`** en la factura seleccionada. Al confirmar la acción en el cuadro de diálogo, la factura se borrará de forma definitiva de la base de datos sin afectar a la persona ni a otras facturas.

<img img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 44 53 PM (4)" src="https://github.com/user-attachments/assets/ffd62ed5-9be6-49a7-bb02-fd70fd861037" />
<img width="300" height="420" alt="WhatsApp Image 2026-05-19 at 10 44 52 PM" src="https://github.com/user-attachments/assets/9f75c351-b24b-4eec-9be1-a7cda5fca76c" />
