CREATE TABLE user_data (
    id INT PRIMARY KEY IDENTITY(1,1),
    username VARCHAR(100) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    level INT DEFAULT 1,
    role VARCHAR(50) NOT NULL CHECK (role IN ('admin', 'user')),
    created_at DATETIME DEFAULT GETDATE(),
	faction INT DEFAULT 1
);

CREATE TABLE trader (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    description TEXT
);

CREATE TABLE quest (
    id INT PRIMARY KEY IDENTITY(1,1),
    title VARCHAR(200) NOT NULL,
    description TEXT,
    required_level INT DEFAULT 0,
    previous_quest_id INT NULL,
    trader_id INT NOT NULL,
    wiki_link VARCHAR(255),
    FOREIGN KEY (previous_quest_id) REFERENCES quest(id),
    FOREIGN KEY (trader_id) REFERENCES trader(id)
);

CREATE TABLE user_quest (
    user_id INT NOT NULL,
    quest_id INT NOT NULL,
    status VARCHAR(50) NOT NULL CHECK (status IN ('NotStarted', 'InProgress', 'Completed')),
    PRIMARY KEY (user_id, quest_id),
    FOREIGN KEY (user_id) REFERENCES user_data(id),
    FOREIGN KEY (quest_id) REFERENCES quest(id)
);

CREATE TABLE item (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL,
    type VARCHAR(100)
);

CREATE TABLE quest_required_item (
    quest_id INT NOT NULL,
    item_id INT NOT NULL,
    amount INT NOT NULL CHECK (amount > 0),
    PRIMARY KEY (quest_id, item_id),
    FOREIGN KEY (quest_id) REFERENCES quest(id),
    FOREIGN KEY (item_id) REFERENCES item(id)
);

CREATE TABLE user_quest_item_progress (
    user_id INT NOT NULL,
    quest_id INT NOT NULL,
    item_id INT NOT NULL,
    amount_collected INT DEFAULT 0,
    PRIMARY KEY (user_id, quest_id, item_id),
    FOREIGN KEY (user_id) REFERENCES user_data(id),
    FOREIGN KEY (quest_id) REFERENCES quest(id),
    FOREIGN KEY (item_id) REFERENCES item(id)
);

CREATE TABLE quest_rewards (
    quest_id INT PRIMARY KEY,
    experience INT DEFAULT 0,
    money INT DEFAULT 0,
    reputation FLOAT DEFAULT 0.0,
    items NVARCHAR(MAX),
    FOREIGN KEY (quest_id) REFERENCES quest(id)
);

CREATE TABLE hideout_station (
    id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(100) NOT NULL
);

CREATE TABLE hideout_station_upgrade (
    station_id INT NOT NULL,
    item_id INT NOT NULL,
    amount INT NOT NULL CHECK (amount > 0),
    required_level INT DEFAULT 0,
    PRIMARY KEY (station_id, item_id),
    FOREIGN KEY (station_id) REFERENCES hideout_station(id),
    FOREIGN KEY (item_id) REFERENCES item(id)
);

CREATE TABLE user_hideout (
    user_id INT NOT NULL,
    station_id INT NOT NULL,
    station_level INT DEFAULT 0,
    PRIMARY KEY (user_id, station_id),
    FOREIGN KEY (user_id) REFERENCES user_data(id),
    FOREIGN KEY (station_id) REFERENCES hideout_station(id)
);
