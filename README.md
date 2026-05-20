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

<!-- Reemplaza el enlace a continuación por tu captura de pantalla de creación de persona -->
![Crear Persona](https://placehold.co/800x450/4f46e5/ffffff?text=Captura+Crear+Persona)

---

### Read (Leer / Listar Personas)
*Descripción del flujo:* Muestra la lista de todas las personas registradas en tarjetas individuales. Incluye una barra de búsqueda para filtrar la lista instantáneamente por nombre.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla del listado de personas -->
![Listado de Personas](https://placehold.co/800x450/4f46e5/ffffff?text=Captura+Listado+Personas)

---

### Update (Actualizar Persona)
*Descripción del flujo:* Presione el botón azul **`Editar`** en la tarjeta de la persona que desea modificar. Se abrirá el formulario cargado con los datos actuales; modifique la información requerida y presione **`Guardar`**.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla de edición de persona -->
![Editar Persona](https://placehold.co/800x450/4f46e5/ffffff?text=Captura+Editar+Persona)

---

### Delete (Eliminar Persona)
*Descripción del flujo:* Presione el botón rojo **`Eliminar`** en la tarjeta de la persona. Se le mostrará un cuadro de diálogo de confirmación. Al pulsar "Sí", el registro y todas sus facturas asociadas se eliminarán por completo.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla de eliminación de persona -->
![Eliminar Persona](https://placehold.co/800x450/4f46e5/ffffff?text=Captura+Eliminar+Persona)

---
---

# Factura

### Create (Crear Factura)
*Descripción del flujo:* Entre al detalle de una persona pulsando **`Ver Facturas`**. Luego, haga clic en el botón verde **`+ Agregar Factura`**, introduzca los datos requeridos (número de factura y monto son obligatorios) y presione **`Guardar`**.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla de creación de factura -->
![Crear Factura](https://placehold.co/800x450/10b981/ffffff?text=Captura+Crear+Factura)

---

### Read (Leer / Listar Facturas de Persona)
*Descripción del flujo:* Dentro de la pantalla de detalle de cada persona, se visualiza la lista de sus facturas registradas en un formato claro que detalla el número, concepto, fecha formateada y el monto de cada transacción en color verde.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla del listado de facturas -->
![Listar Facturas](https://placehold.co/800x450/10b981/ffffff?text=Captura+Listado+Facturas)

---

### Update (Actualizar Factura)
*Descripción del flujo:* En la lista de facturas de una persona, pulse el botón azul **`Editar`** correspondiente a la factura que desea modificar, realice los cambios necesarios y presione **`Guardar`**.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla de edición de factura -->
![Editar Factura](https://placehold.co/800x450/10b981/ffffff?text=Captura+Editar+Factura)

---

### Delete (Eliminar Factura)
*Descripción del flujo:* Pulse el botón rojo **`Eliminar`** en la factura seleccionada. Al confirmar la acción en el cuadro de diálogo, la factura se borrará de forma definitiva de la base de datos sin afectar a la persona ni a otras facturas.

<!-- Reemplaza el enlace a continuación por tu captura de pantalla de eliminación de factura -->
![Eliminar Factura](https://placehold.co/800x450/10b981/ffffff?text=Captura+Eliminar+Factura)
