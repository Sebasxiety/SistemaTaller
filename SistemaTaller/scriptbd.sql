USE [BBDDTaller]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[IdCliente] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[Apellido] [nvarchar](50) NOT NULL,
	[Telefono] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[FechaRegistro] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdCliente] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contratos]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contratos](
	[IdContrato] [int] IDENTITY(1,1) NOT NULL,
	[IdCliente] [int] NOT NULL,
	[IdVehiculo] [int] NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [datetime] NULL,
	[MontoTotal] [decimal](10, 2) NOT NULL,
	[FechaCreacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdContrato] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pagos]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pagos](
	[IdPago] [int] IDENTITY(1,1) NOT NULL,
	[IdContrato] [int] NOT NULL,
	[Monto] [decimal](10, 2) NOT NULL,
	[FechaPago] [datetime] NOT NULL,
	[MetodoPago] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPago] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vehiculos]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehiculos](
	[IdVehiculo] [int] IDENTITY(1,1) NOT NULL,
	[Marca] [nvarchar](50) NOT NULL,
	[Modelo] [nvarchar](50) NOT NULL,
	[Anio] [int] NOT NULL,
	[Color] [nvarchar](30) NULL,
	[Estado] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdVehiculo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clientes] ADD  DEFAULT (getdate()) FOR [FechaRegistro]
GO
ALTER TABLE [dbo].[Contratos] ADD  DEFAULT (getdate()) FOR [FechaCreacion]
GO
ALTER TABLE [dbo].[Pagos] ADD  DEFAULT (getdate()) FOR [FechaPago]
GO
ALTER TABLE [dbo].[Vehiculos] ADD  DEFAULT ('Disponible') FOR [Estado]
GO
ALTER TABLE [dbo].[Contratos]  WITH CHECK ADD  CONSTRAINT [FK_Contratos_Clientes] FOREIGN KEY([IdCliente])
REFERENCES [dbo].[Clientes] ([IdCliente])
GO
ALTER TABLE [dbo].[Contratos] CHECK CONSTRAINT [FK_Contratos_Clientes]
GO
ALTER TABLE [dbo].[Contratos]  WITH CHECK ADD  CONSTRAINT [FK_Contratos_Vehiculos] FOREIGN KEY([IdVehiculo])
REFERENCES [dbo].[Vehiculos] ([IdVehiculo])
GO
ALTER TABLE [dbo].[Contratos] CHECK CONSTRAINT [FK_Contratos_Vehiculos]
GO
ALTER TABLE [dbo].[Pagos]  WITH CHECK ADD  CONSTRAINT [FK_Pagos_Contratos] FOREIGN KEY([IdContrato])
REFERENCES [dbo].[Contratos] ([IdContrato])
GO
ALTER TABLE [dbo].[Pagos] CHECK CONSTRAINT [FK_Pagos_Contratos]
GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarCliente]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarCliente]
    @IdCliente  INT,
    @Nombre     NVARCHAR(50),
    @Apellido   NVARCHAR(50),
    @Telefono   NVARCHAR(20),
    @Email      NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Clientes
    SET Nombre = @Nombre,
        Apellido = @Apellido,
        Telefono = @Telefono,
        Email = @Email
    WHERE IdCliente = @IdCliente;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarContrato]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarContrato]
    @IdContrato  INT,
    @IdCliente   INT,
    @IdVehiculo  INT,
    @FechaInicio DATETIME,
    @FechaFin    DATETIME,
    @MontoTotal  DECIMAL(10, 2)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Contratos
    SET IdCliente   = @IdCliente,
        IdVehiculo  = @IdVehiculo,
        FechaInicio = @FechaInicio,
        FechaFin    = @FechaFin,
        MontoTotal  = @MontoTotal
    WHERE IdContrato = @IdContrato;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarPago]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarPago]
    @IdPago     INT,
    @IdContrato INT,
    @Monto      DECIMAL(10, 2),
    @FechaPago  DATETIME,
    @MetodoPago NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Pagos
    SET IdContrato = @IdContrato,
        Monto      = @Monto,
        FechaPago  = @FechaPago,
        MetodoPago = @MetodoPago
    WHERE IdPago = @IdPago;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ActualizarVehiculo]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ActualizarVehiculo]
    @IdVehiculo INT,
    @Marca      NVARCHAR(50),
    @Modelo     NVARCHAR(50),
    @Anio       INT,
    @Color      NVARCHAR(30),
    @Estado     NVARCHAR(20)
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Vehiculos
    SET Marca = @Marca,
        Modelo = @Modelo,
        Anio = @Anio,
        Color = @Color,
        Estado = @Estado
    WHERE IdVehiculo = @IdVehiculo;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarCliente]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarCliente]
    @IdCliente INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Clientes
    WHERE IdCliente = @IdCliente;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarContrato]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarContrato]
    @IdContrato INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Contratos
    WHERE IdContrato = @IdContrato;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarPago]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarPago]
    @IdPago INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Pagos
    WHERE IdPago = @IdPago;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_EliminarVehiculo]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_EliminarVehiculo]
    @IdVehiculo INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM Vehiculos
    WHERE IdVehiculo = @IdVehiculo;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarCliente]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarCliente]
    @Nombre     NVARCHAR(50),
    @Apellido   NVARCHAR(50),
    @Telefono   NVARCHAR(20),
    @Email      NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Clientes (Nombre, Apellido, Telefono, Email)
    VALUES (@Nombre, @Apellido, @Telefono, @Email);

    SELECT SCOPE_IDENTITY() AS IdClienteCreado;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarContrato]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarContrato]
    @IdCliente   INT,
    @IdVehiculo  INT,
    @FechaInicio DATETIME,
    @FechaFin    DATETIME,
    @MontoTotal  DECIMAL(10, 2)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Contratos (IdCliente, IdVehiculo, FechaInicio, FechaFin, MontoTotal)
    VALUES (@IdCliente, @IdVehiculo, @FechaInicio, @FechaFin, @MontoTotal);

    SELECT SCOPE_IDENTITY() AS IdContratoCreado;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarPago]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarPago]
    @IdContrato INT,
    @Monto      DECIMAL(10, 2),
    @FechaPago  DATETIME,
    @MetodoPago NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Pagos (IdContrato, Monto, FechaPago, MetodoPago)
    VALUES (@IdContrato, @Monto, @FechaPago, @MetodoPago);

    SELECT SCOPE_IDENTITY() AS IdPagoCreado;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_InsertarVehiculo]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_InsertarVehiculo]
    @Marca     NVARCHAR(50),
    @Modelo    NVARCHAR(50),
    @Anio      INT,
    @Color     NVARCHAR(30),
    @Estado    NVARCHAR(20) = 'Disponible'
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO Vehiculos (Marca, Modelo, Anio, Color, Estado)
    VALUES (@Marca, @Modelo, @Anio, @Color, @Estado);

    SELECT SCOPE_IDENTITY() AS IdVehiculoCreado;
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerClientes]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerClientes]
    @IdCliente INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdCliente IS NULL
    BEGIN
        SELECT * FROM Clientes;
    END
    ELSE
    BEGIN
        SELECT * FROM Clientes
        WHERE IdCliente = @IdCliente;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerContratos]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerContratos]
    @IdContrato INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdContrato IS NULL
    BEGIN
        SELECT c.*,
               cli.Nombre, cli.Apellido,
               v.Marca, v.Modelo
        FROM Contratos c
        INNER JOIN Clientes cli ON c.IdCliente = cli.IdCliente
        INNER JOIN Vehiculos v ON c.IdVehiculo = v.IdVehiculo;
    END
    ELSE
    BEGIN
        SELECT c.*,
               cli.Nombre, cli.Apellido,
               v.Marca, v.Modelo
        FROM Contratos c
        INNER JOIN Clientes cli ON c.IdCliente = cli.IdCliente
        INNER JOIN Vehiculos v ON c.IdVehiculo = v.IdVehiculo
        WHERE c.IdContrato = @IdContrato;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerPagos]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerPagos]
    @IdPago INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdPago IS NULL
    BEGIN
        SELECT p.*,
               c.IdCliente,
               c.IdVehiculo
        FROM Pagos p
        INNER JOIN Contratos c ON p.IdContrato = c.IdContrato;
    END
    ELSE
    BEGIN
        SELECT p.*,
               c.IdCliente,
               c.IdVehiculo
        FROM Pagos p
        INNER JOIN Contratos c ON p.IdContrato = c.IdContrato
        WHERE p.IdPago = @IdPago;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ObtenerVehiculos]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[sp_ObtenerVehiculos]
    @IdVehiculo INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @IdVehiculo IS NULL
    BEGIN
        SELECT * FROM Vehiculos;
    END
    ELSE
    BEGIN
        SELECT * FROM Vehiculos
        WHERE IdVehiculo = @IdVehiculo;
    END
END;

GO
/****** Object:  StoredProcedure [dbo].[sp_ReporteContratosPorCliente]    Script Date: 01/02/2025 6:54:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[sp_ReporteContratosPorCliente]
    @IdCliente INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        c.IdContrato,
        c.IdCliente,
        cli.Nombre AS NombreCliente,
        cli.Apellido AS ApellidoCliente,
        v.Marca,
        v.Modelo,
        c.FechaInicio,
        c.FechaFin,
        c.MontoTotal
    FROM Contratos c
    INNER JOIN Clientes cli ON c.IdCliente = cli.IdCliente
    INNER JOIN Vehiculos v ON c.IdVehiculo = v.IdVehiculo
    WHERE (@IdCliente IS NULL OR c.IdCliente = @IdCliente)
    ORDER BY c.FechaInicio DESC;
END;

GO
