USE TaskTracker;
GO

SET NOCOUNT ON;

-- Limpia datos existentes (respetando FKs) para poder correr el script varias veces
DELETE FROM Task;
DELETE FROM Milestone;
DELETE FROM Project;
DELETE FROM [User];
DBCC CHECKIDENT ('Task', RESEED, 0);
DBCC CHECKIDENT ('Milestone', RESEED, 0);
DBCC CHECKIDENT ('Project', RESEED, 0);
DBCC CHECKIDENT ('[User]', RESEED, 0);
GO

-- ===================== Users =====================
INSERT INTO [User] (Name, Email, IsActive) VALUES
('Ana Martinez',      'ana.martinez@tasktracker.com',      1),
('Carlos Rojas',      'carlos.rojas@tasktracker.com',      1),
('Beatriz Vargas',    'beatriz.vargas@tasktracker.com',    1),
('Diego Fernandez',   'diego.fernandez@tasktracker.com',   1),
('Elena Castillo',    'elena.castillo@tasktracker.com',    1),
('Jorge Ramirez',     'jorge.ramirez@tasktracker.com',     0);
GO

-- ===================== Projects =====================
INSERT INTO Project (Name, Description, Objetive, Team, EstimatedTimeOfCompletion) VALUES
('Sistema de Gestion de Inventario',
 'Plataforma web para el control de stock, entradas y salidas de almacen.',
 'Reducir en un 30% los errores de conteo manual de inventario.',
 'Ana Martinez, Carlos Rojas, Beatriz Vargas',
 '3 meses'),
('Portal de Recursos Humanos',
 'Portal interno para gestion de vacaciones, planillas y evaluaciones de desempeno.',
 'Centralizar los procesos de RRHH en una sola plataforma.',
 'Diego Fernandez, Elena Castillo',
 '4 meses'),
('App Movil de Ventas',
 'Aplicacion movil para que la fuerza de ventas registre pedidos en campo.',
 'Aumentar la velocidad de captura de pedidos en un 50%.',
 'Ana Martinez, Diego Fernandez, Jorge Ramirez',
 '2 meses');
GO

-- ===================== Milestones =====================
-- Referenciamos el proyecto por nombre para no depender de valores de IDENTITY
INSERT INTO Milestone (ProjectId, Name, Status)
SELECT p.ProjectId, v.Name, v.Status
FROM (VALUES
    ('Sistema de Gestion de Inventario', 'Analisis y Diseno',        3), -- Completed
    ('Sistema de Gestion de Inventario', 'Desarrollo del Backend',   2), -- InProgress
    ('Sistema de Gestion de Inventario', 'Pruebas y Despliegue',     1), -- Pending
    ('Portal de Recursos Humanos',       'Levantamiento de Requerimientos', 3), -- Completed
    ('Portal de Recursos Humanos',       'Modulo de Vacaciones',            2), -- InProgress
    ('App Movil de Ventas',              'Prototipo UI/UX',       3), -- Completed
    ('App Movil de Ventas',              'Integracion con API',   2)  -- InProgress
) AS v(ProjectName, Name, Status)
JOIN Project p ON p.Name = v.ProjectName;
GO

-- ===================== Tasks =====================
-- Referenciamos milestone (por proyecto+nombre) y usuarios (por nombre) para no depender de IDs fijos
INSERT INTO Task (Name, Description, MilestoneId, Status, CreatedDate, DueDate, ModifiedDate, ResponsibleId, AssigneeId, Priority, Comments)
SELECT
    v.TaskName, v.Description, m.Id, v.Status,
    v.CreatedDate, v.DueDate, v.ModifiedDate,
    ru.UserId, au.UserId,
    v.Priority, v.Comments
FROM (VALUES
    -- ProjectName, MilestoneName, TaskName, Description, Status, CreatedDate, DueDate, ModifiedDate, ResponsibleName, AssigneeName, Priority, Comments
    ('Sistema de Gestion de Inventario', 'Analisis y Diseno',      'Levantar requerimientos con almacen', 'Reunion con el equipo de almacen para definir alcance.', 3, '2026-06-01', '2026-06-10', '2026-06-09', 'Ana Martinez',    'Carlos Rojas',   2, 'Cerrado sin observaciones.'),
    ('Sistema de Gestion de Inventario', 'Analisis y Diseno',      'Disenar modelo de datos',             'Diseno del esquema de base de datos del inventario.',    3, '2026-06-05', '2026-06-15', '2026-06-14', 'Carlos Rojas',    'Carlos Rojas',   3, NULL),

    ('Sistema de Gestion de Inventario', 'Desarrollo del Backend', 'Implementar API de productos',   'CRUD de productos con validaciones de stock.',      2, '2026-06-16', '2026-08-20', '2026-08-08', 'Carlos Rojas',    'Beatriz Vargas', 3, 'En progreso, falta validacion de stock minimo.'),
    ('Sistema de Gestion de Inventario', 'Desarrollo del Backend', 'Implementar API de movimientos', 'Registrar entradas y salidas de inventario.',       1, '2026-06-20', '2026-08-25', '2026-06-20', 'Carlos Rojas',    'Beatriz Vargas', 2, NULL),
    ('Sistema de Gestion de Inventario', 'Desarrollo del Backend', 'Revision de seguridad del API',  'Revisar autenticacion y autorizacion del backend.', 4, '2026-06-25', '2026-07-30', '2026-08-01', 'Ana Martinez',    'Diego Fernandez', 4, 'Atrasada, pendiente de asignar recursos.'),

    ('Sistema de Gestion de Inventario', 'Pruebas y Despliegue',   'Preparar plan de pruebas', 'Definir casos de prueba funcionales.', 1, '2026-08-01', '2026-09-05', '2026-08-01', 'Beatriz Vargas', NULL, 2, NULL),

    ('Portal de Recursos Humanos', 'Levantamiento de Requerimientos', 'Entrevistas con RRHH',         'Entrevistar a jefaturas de RRHH sobre procesos actuales.', 3, '2026-05-10', '2026-05-20', '2026-05-19', 'Diego Fernandez', 'Elena Castillo', 1, NULL),
    ('Portal de Recursos Humanos', 'Levantamiento de Requerimientos', 'Documentar procesos actuales', 'Documentar el flujo de vacaciones y planillas.',           3, '2026-05-15', '2026-05-25', '2026-05-24', 'Elena Castillo',  'Elena Castillo', 2, NULL),

    ('Portal de Recursos Humanos', 'Modulo de Vacaciones', 'Disenar flujo de aprobacion', 'Flujo de solicitud y aprobacion de vacaciones.',        2, '2026-06-01', '2026-08-15', '2026-08-05', 'Diego Fernandez', 'Elena Castillo', 3, 'En revision con el cliente.'),
    ('Portal de Recursos Humanos', 'Modulo de Vacaciones', 'Notificaciones por correo',   'Enviar notificaciones al aprobar/rechazar solicitudes.', 4, '2026-06-10', '2026-07-15', '2026-07-16', 'Elena Castillo',  'Elena Castillo', 2, 'Atrasada por dependencia del flujo de aprobacion.'),

    ('App Movil de Ventas', 'Prototipo UI/UX', 'Wireframes de pantallas principales', 'Wireframes de login, catalogo y pedido.', 3, '2026-04-01', '2026-04-15', '2026-04-14', 'Ana Martinez', 'Ana Martinez', 2, NULL),

    ('App Movil de Ventas', 'Integracion con API', 'Conectar catalogo de productos', 'Consumir API de productos existente.',           2, '2026-07-01', '2026-08-20', '2026-08-07', 'Diego Fernandez', 'Ana Martinez',    3, NULL),
    ('App Movil de Ventas', 'Integracion con API', 'Sincronizar pedidos offline',    'Cola de sincronizacion cuando no hay conexion.', 1, '2026-07-10', '2026-09-01', '2026-07-10', 'Diego Fernandez', 'Diego Fernandez', 4, NULL)
) AS v(ProjectName, MilestoneName, TaskName, Description, Status, CreatedDate, DueDate, ModifiedDate, ResponsibleName, AssigneeName, Priority, Comments)
JOIN Project p ON p.Name = v.ProjectName
JOIN Milestone m ON m.ProjectId = p.ProjectId AND m.Name = v.MilestoneName
JOIN [User] ru ON ru.Name = v.ResponsibleName
LEFT JOIN [User] au ON au.Name = v.AssigneeName;
GO

PRINT 'Seed data insertada correctamente.';

SELECT 'Users' AS Tabla, COUNT(*) AS Filas FROM [User]
UNION ALL SELECT 'Projects', COUNT(*) FROM Project
UNION ALL SELECT 'Milestones', COUNT(*) FROM Milestone
UNION ALL SELECT 'Tasks', COUNT(*) FROM Task;
GO
