# Cadeteria2024

## Trabajo Practico 005

## Consignas

1) Desde el repositorio utilizado en el TP 4, cree una nueva rama llamada TP5 para implementar
lo solicitado.
2) Migración del Sistema para Cadeteria
Se desea ahora implementar una nueva capa de acceso a datos en preparación a la
integración con una base de datos.
Para poder cumplir con dicho requisito se decidió separar la capa de acceso a datos en
clases específicas para cada entidad. Para tal fin debe implementar:
a) Una nueva clase AccesoADatosCadeteria con los siguientes métodos:
i) Cadeteria Obtener()
b) Una nueva clase AccesoADatosCadetes con los siguientes métodos:
i) List<Cadetes> Obtener()
Por otra parte, se desea además agregar la funcionalidad de guardar Pedidos. Para ellos
cree una nueva clase AccesoADatosPedidos con los siguientes métodos:
ii) List<Pedidos> Obtener()
iii) void Guardar(List<Pedidos> Pedidos)
