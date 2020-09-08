-- USE YOUR-DB-HERE;

CREATE TABLE IF NOT EXISTS vaults (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    INDEX userId (userId),  
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS keeps (
    id int NOT NULL AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    description VARCHAR(255) NOT NULL,
    userId VARCHAR(255),
    img VARCHAR(255),
    isPrivate TINYINT,
    views INT DEFAULT 0,
    shares INT DEFAULT 0,
    keeps INT DEFAULT 0,
    INDEX userId (userId),
    PRIMARY KEY (id)
);

CREATE TABLE IF NOT EXISTS vaultkeeps (
    id int NOT NULL AUTO_INCREMENT,
    vaultId int NOT NULL,
    keepId int NOT NULL,
    vkuserId VARCHAR(255) NOT NULL,

    PRIMARY KEY (id),
    INDEX (vaultId, keepId),
    INDEX (vkuserId),

    FOREIGN KEY (vaultId)
        REFERENCES vaults(id)
        ON DELETE CASCADE,

    FOREIGN KEY (keepId)
        REFERENCES keeps(id)
        ON DELETE CASCADE
);


DELETE FROM vaultkeeps WHERE id = 36;


-- ALTER TABLE vaultkeeps
-- ADD COLUMN vkuserId VARCHAR(255) DEFAULT "" NOT NULL

-- USE THIS LINE FOR GET KEEPS BY VAULTID
-- SELECT 
-- k.*,
-- vk.id as vaultKeepId
-- FROM vaultkeeps vk
-- INNER JOIN keeps k ON k.id = vk.keepId 
-- WHERE (vaultId = @vaultId AND vk.userId = @userId) 



-- -- USE THIS TO CLEAN OUT YOUR DATABASE
-- DROP TABLE IF EXISTS vaultkeeps;
-- DROP TABLE IF EXISTS vaults;
-- DROP TABLE IF EXISTS keeps;
-- DROP TABLE IF EXISTS users;
