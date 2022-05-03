# trackingApi
Este repositorio es para el reto propuesto en el proceso de seleccion de Recereativos Franco Digital

Este repositorio es el segundo intento después de https://github.com/Josensei/SeguimientoAPI 


Nivel 1:
Al principio gestionaba las consultas usando Postman, por comodidad, ahora mismo está configurado con Swagger.
En este Swagger se pueden ver todas las funciones disponibles. 
El primer intento no contenia ningun tipo de persistencia, usaba un Dbcontext para almacenar datos en memoria que me daba bastantes problemas,
eso fue el motivo para cambiar a este segundo intento, utilizo la plantilla de visual studio para web APIs.

Las funciones están divididas en Pedidos y Vehiculos, En pedidos se gestiona principalmente la creacion de estos, así como la modificacion de los mismos 
o incluso su eliminacion si fuera necesario, todo lo que ocurre en estas peticiones se persiste en una BDD, de mongoDB https://www.mongodb.com/(excepto los get). 
Como funcionalidad añadida, la clase pedido tiene un atributo estado, si este estado cambia se enviaría un correo al email asociado al pedido.

Vehiculos tiene las mismas funciones que pedidos y otras añadidas, estas funcionalidades son
GET distance: El vehiculo cogería la direccion del pedido y su última  ubicacion y calcularía la distancia entre ambos puntos, 
desafortunadamente no he podido darle bien la funcionalidad a esta èticion porque la API de Google me pedía registrar una tarjeta de crédito,
he visto la API de Google y he visto que no sería complicado pero no he podido hacerlo.

Put Location: agrega localizacoines mediante coordenadas GPS, se comprueba el formato de als coordenadas antes de añadirlas.

Put Pedido: agrega el ID de un pedido al vehiculo, empecé haciendolo con el objeto, pero pensé que no tenia sentido, debido a que entonces
havbría que hacer cascada con los cambios de informacion del pedido.

Put DropPedido: Localiza un ID de pedido en la lista de pedidos del vehiculo y lo saca de la misma.


Nivel 2: Para el nivel 2 pensé en utilizar un websocket, vi que aparentemente usando la libreria SignalR podia conseguir lo que necesitaba, tras varios intentos
no he conseguido agregar la funcionalidad al programa, pero a raiz de pensar alternativas surgió la funcionalidad añadida de enviar el correo en un cambio de estado. 
No es una actualizacion en tiempo real, pero da informacion al usuarto del servicio.
