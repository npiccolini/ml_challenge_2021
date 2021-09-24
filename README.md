# Proyecto - Servicio Rest para detectar mutantes
El proyecto consiste en generar una API-REST que exponga dos servicios, uno te permite saber si una persona es humano o mutante basado en su ADN representado por una matriz de NxN caracteres y también un servicio que arroje estadísticas en función de los ADN estudiados, los cuales son mutantes y o no.

--------

## Contenido

- [Arquitectura utilizada](#arquitectura)
- [Tecnologías y herramientas](#herramientas)
- [Instalacion](#instalación)
- [Api](#api)
- [Ejemplos](#ejemplos)
- [Consideraciones](#consideraciones)
- [Licencia](#licencia)
- 
---
# Arquitectura

  - Es una arquitectura orientada a servicios. Tenemos controlador, servicio, manager, repositorio, excepciones y entidades.
----
# Herramientas

 * [C#] - Lenguaje de programación 
 * [Git] - Versionado
 * [Nugget] - Paquetización y dependencias
 * [NET.CORE] - Framework de trabajo
 * [VS Studio] - Ide de desarrollo
 * [Azure] - Servidor en la nube
 * [Sqlite] - Base de datos
 * [GitHub] - Repositorio y manual de uso
 * [MSTest] - Framework para testing
 * [Moq] - Para Mocker servicios para testing
---
# Instalación

- Básicamente se necesita tener esta tecnología instalada en el server.

| Requiere |
| ------ |
| Net.Core 3.1 |
| Git |
| VS Studio|


Luego en el espacio de trabajo o workspace clonar el proyecto:
```sh
$ git clone https://github.com/npiccolini/ml_challenge_2021.git
```

- Abrir con el visual studio
- Compilar (y restaurar paquetenes nugget)
- Cambiar el connectionstring de MeliContext del appsettings.json (apuntar al directiorio local donde esta Meli.db)

------

# API

- La aplicación  está configurada por defecto para que use el iisexpress, esto se puede cambiar en las configuraciones del proyecto.
- Actualmente hay una instancia de la aplicación corriendo en un servidor de azure.

| DESCRIPCION  | URL | PETICION  | HEADER  | RESPUESTA
| ------ | ------ | ------ | ------ | ------ |
| Servicio Mutant | https://mlchallenge2021.azurewebsites.net/mutant | POST | Content-Type: application/json | Devuelve 200 si es mutant o 403 en caso contrario
| Servicio Stats | https://mlchallenge2021.azurewebsites.net/stats | GET |   | JSON

------

# Ejemplos 


	1) SERVICIO: mutant 
  	   REQUEST: [TYPE POST; HEADER Content-Type: application/json]
    	{
    	"dna": ["ATGCGA",
    		"CAGTGG",
    		"TCCCCT",
    		"ATAGGG",
    		"CCTAAA",
    		"TCATTG"
    	]}
	   RESPONSE: 200 - OK

	2) SERVICIO: stats
	   RESPONSE: 200 - OK 
	{ "count_mutant_dna": 2, "count_human_dna": 2, "ratio": 1}	

------

# Consideraciones
- Se validaron las diferentes secuencias de una matriz de DNA.
- Se validó la estructura respetara la uniformidad NxN
- Se colocó como condición mínima para que el DNA sea de mutante que se obtuvieron al menos 2 secuencias de 4 caracteres seguidos en la matriz de DNA
- La secuencia de DNA es estudiada primero en forma horizontal, luego vertical,
luego en sus diagonales.
- Hay dos servicios expuestos para poder ver listado de ADN estudiados.
- En cuanto al punto de: "Tener en cuenta que la API puede recibir fluctuaciones agresivas de tráfico 
 (Entre 100 y 1 millón de peticiones por segundo)"; Hay varias consideraciones a tener en cuenta:
Lo idea seria contar con mas informacion ya que, si bien el POST /mutant es el endpoint que mas llamadas va a recibir, hay que considerar, la performance del código, por un lado, y por otro la capacidad de un millón de transacciones por segundo puede que se necesiten más de un host.
Tambien debe revisarse la arquitectura, ya que las respuestas pueden no ser necesarias en tiempo real o podrian encolarse.
Dependiendo de esto, si no es necesario que sea en tiempo real entonces puede colocarse una cola que vaya guardando las operaciones pendientes mientras los servidores están ocupados e ir procesando en ese orden y notificar cuando la respuesta este lista. Si las operaciones no se pueden hacer de manera asíncrona entonces hay que desplegar una capacidad capaz de soportar esta cantidad de transacciones (balanceadores de carga, múltiples hosts, por ejemplo).

----

# Licencia


**Software libre**
