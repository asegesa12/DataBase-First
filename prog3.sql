use SGRH

go

create table departamentos(
id_departamento int not null constraint pk_departamentos primary key,
codigoDepartamento varchar(10),
nombre varchar(40),
encargado varchar(40)
)

go

create table cargos(
id_cargos int not null constraint pk_cargos primary key,
codigoCargo varchar(10),
cargo varchar(40)
)

go

create table empleados(
id int not null constraint pk_empleados primary key,
codigoEmpleado varchar(10),
nombre varchar(40),
apellido varchar(40),
telefono varchar(20),
fechaIngreso date default GetDate(),
salario int,
estado varchar(8) check(estado in('activo', 'inactivo')),
id_departamento int not null, constraint fk_emp_dep foreign key (id_departamento) references departamentos,
id_cargos int not null, constraint fk_emp_car foreign key (id_cargos) references cargos
)

go

create table nomina(
id_nomina int identity(1,1) not null constraint pk_nomina primary key,
año varchar(4),
mes varchar(15),
montoTotal int
)

go

create table salidaEmpleado(
id_salida int identity(1,1) not null constraint pk_salida primary key,
tipoSalida varchar(10) check(tipoSalida in('Renuncia', 'Despido', 'Desahucio')),
motivo varchar(130),
fechaSalida date default GetDate(),
id_empleado int not null, constraint fk_sal_emp foreign key (id_empleado) references empleados
)

go

create table vacaciones(
id_vacaciones int identity(1,1) not null constraint pk_vacaciones primary key,
desde date default GetDate(),
hasta date default GetDate(),
año varchar(4),
comentario varchar(130),
id_empleado int not null, constraint fk_vac_emp foreign key (id_empleado) references empleados
)

go

create table permisos(
id_permiso int identity(1,1) not null constraint pk_permisos primary key,
desde date default GetDate(),
hasta date default GetDate(),
comentario varchar(130),
id_empleado int not null, constraint fk_per_emp foreign key (id_empleado) references empleados
)

go

create table licencias(
id_licencia int identity(1,1) not null constraint pk_licencias primary key,
desde date default GetDate(),
hasta date default GetDate(),
motivo varchar(130),
comentarios varchar(130),
id_empleado int not null, constraint fk_lic_emp foreign key (id_empleado) references empleados
)

go

-- Creacion de trigger para inactivar a un empleado por medio de salida

create trigger trg_ins_salida on salidaEmpleado
	after insert
	as
	begin

	declare @id_empleado int
	declare @id_salida int

	select @id_salida = MAX(id_salida) from salidaEmpleado

	select @id_empleado = id_empleado from salidaEmpleado where id_salida = @id_salida

	update empleados set estado = 'inactivo' where id = @id_empleado

	end

go

-- Prueba del trigger

insert into departamentos values (1, 'D12', 'Recursos Humanos', 'fulano de tal')

insert into cargos values (1, 'C11', 'Gerente')

insert into empleados values (2, 'A15', 'Hector', 'Hernandez', '829-610-5929', '2019-07-04', 15000, 'activo', 1, 1)

select * from empleados

insert into salidaEmpleado values('Renuncia', 'no a gusto', '2019-04-24', 1)

select * from empleados

--  Fin prueba trigger

create table entradaEmpleados(
id_entrada int identity(1,1) not null constraint pk_entrada primary key,
mes varchar(15),
id_empleado int not null, constraint fk_ent_emp foreign key (id_empleado) references empleados
)

go

select datename(month, fechaIngreso) from empleados where id = 1

go

create trigger trg_ins_emp on empleados
after insert
as
begin

declare @id_empleado int
declare @mes varchar(15)

select @id_empleado = MAX(id) from empleados

select @mes = datename(month, fechaIngreso) from empleados where id = @id_empleado

insert into entradaEmpleados values (@mes, @id_empleado)

end

go

create trigger trg_iud_emp on empleados
after insert, update, delete
as
begin

declare @montoTotal int
declare @año varchar(4)
declare @mes varchar(15)
declare @mesNomina varchar(15)
declare @id_nomina int

select @montoTotal = sum(salario) from empleados where estado = 'activo'

select @año = year(GETDATE())

select @mes = datename(month, GETDATE())

select @id_nomina = MAX(id_nomina) from nomina

select @mesNomina = mes from nomina where id_nomina = @id_nomina

if @mes = @mesNomina
begin

update nomina set montoTotal = @montoTotal, año = @año, mes = @mes

end else
begin

insert into nomina values (@año, @mes, @montoTotal)

end

end

go

insert into empleados values (1, 'Q145', 'Hector', 'Hernandez', '829-610-5929', '2019-05-7', 15000, 'activo', 1, 1)


