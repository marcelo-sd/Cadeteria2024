# Cadeteria2024

## Trabajo Practico 005

## Consignas

1) Desde el repositorio utilizado en el TP 4, cree una nueva rama llamada TP5 para implementar
lo solicitado.
2) Migraci�n del Sistema para Cadeteria
Se desea ahora implementar una nueva capa de acceso a datos en preparaci�n a la
integraci�n con una base de datos.
Para poder cumplir con dicho requisito se decidi� separar la capa de acceso a datos en
clases espec�ficas para cada entidad. Para tal fin debe implementar:
a) Una nueva clase AccesoADatosCadeteria con los siguientes m�todos:
i) Cadeteria Obtener()
b) Una nueva clase AccesoADatosCadetes con los siguientes m�todos:
i) List<Cadetes> Obtener()
Por otra parte, se desea adem�s agregar la funcionalidad de guardar Pedidos. Para ellos
cree una nueva clase AccesoADatosPedidos con los siguientes m�todos:
ii) List<Pedidos> Obtener()
iii) void Guardar(List<Pedidos> Pedidos)
