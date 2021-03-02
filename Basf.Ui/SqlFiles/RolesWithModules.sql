delete from dbo.RolesAndAccessibleModules
go

INSERT dbo.RolesAndAccessibleModules (RoleName, ModulesAccessible) VALUES ('Admin', 'UserManagement')
INSERT dbo.RolesAndAccessibleModules (RoleName, ModulesAccessible) VALUES ('Admin', 'DesignExperiment')
INSERT dbo.RolesAndAccessibleModules (RoleName, ModulesAccessible) VALUES ('Admin', 'ReactorControl')

INSERT dbo.RolesAndAccessibleModules (RoleName, ModulesAccessible) VALUES ('Scientist', 'DesignExperiment')
INSERT dbo.RolesAndAccessibleModules (RoleName, ModulesAccessible) VALUES ('Scientist', 'ReactorControl')

INSERT dbo.RolesAndAccessibleModules (RoleName, ModulesAccessible) VALUES ('Viewer', 'ReactorControl')

