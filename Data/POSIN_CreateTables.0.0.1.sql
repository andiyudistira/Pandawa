CREATE TABLE Users (
  UserId        integer PRIMARY KEY NOT NULL UNIQUE,
  UserName      text NOT NULL,
  Password      text NOT NULL,
  RecordStatus  bit NOT NULL DEFAULT '1'
);

CREATE TABLE Roles (
  RoleId        integer PRIMARY KEY NOT NULL UNIQUE,
  RoleName      text NOT NULL,
  InsertBy      integer NOT NULL,
  InsertDate    datetime NOT NULL,
  UpdateBy      integer,
  UpdateDate    datetime,
  RecordStatus  bit NOT NULL DEFAULT '1'
);

CREATE TABLE UsersInRoles (
  UsersInRoleId integer PRIMARY KEY AUTOINCREMENT NOT NULL UNIQUE,
  UserId  text NOT NULL,
  RoleId  text NOT NULL,
  /* Foreign keys */
  FOREIGN KEY (RoleId)
    REFERENCES Roles(RoleId)
    ON DELETE CASCADE
    ON UPDATE NO ACTION, 
  FOREIGN KEY (UserId)
    REFERENCES Users(UserId)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
);

CREATE TABLE UserSession (
  SessionId    guid PRIMARY KEY NOT NULL UNIQUE,
  UserId       integer NOT NULL,
  LoginDate    datetime NOT NULL,
  LogOffDate   datetime NOT NULL,
  LoginStatus  integer NOT NULL DEFAULT 0,
  /* Foreign keys */
  FOREIGN KEY (UserId)
    REFERENCES Users(UserId)
    ON DELETE CASCADE
    ON UPDATE NO ACTION
);

CREATE INDEX IX_Users
  ON Users
  (UserId, UserName);

CREATE INDEX IX_Roles
  ON Roles
  (RoleId, RoleName);

CREATE INDEX IX_UsersInRoles
  ON UsersInRoles
  (UserId, RoleId);

CREATE INDEX IX_UserHistory
  ON UserSession
  (UserId, LoginDate, LogOffDate);