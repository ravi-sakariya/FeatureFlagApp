-- Create a new database
CREATE DATABASE FeatureFlagDB;
GO

-- Switch to the new database
USE FeatureFlagDB;
GO

-- Create the FeatureFlags table
CREATE TABLE FeatureFlags (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Enabled BIT NOT NULL,
    TargetUsersCsv NVARCHAR(MAX) NULL,
    TargetGroupsCsv NVARCHAR(MAX) NULL
);
GO

-- Insert two temporary flags
INSERT INTO FeatureFlags (Name, Enabled, TargetUsersCsv, TargetGroupsCsv)
VALUES 
    ('Dashoboard', 1, 'user123,betaTester42', ''),
    ('BetaFeature', 0, 'vipUser1,vipUser2', 'admin');
GO

-- Verify inserted flags
SELECT * FROM FeatureFlags;
GO