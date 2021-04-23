CREATE TABLE PartOfSpeech(
    id INT IDENTITY(1,1) PRIMARY KEY,
    part_name VARCHAR(30) NOT NULL,
);

CREATE TABLE EnglishWords(
    id INT IDENTITY(1,1) PRIMARY KEY,
    word VARCHAR(50) NOT NULL UNIQUE,
    part_of_speech_id INT,
	created_at DATETIME2(0),
    FOREIGN KEY (part_of_speech_id) REFERENCES PartOfSpeech(id) ON DELETE SET NULL ON UPDATE CASCADE,
);

CREATE TABLE RussianWords(
    id INT IDENTITY(1,1) PRIMARY KEY,
    word VARCHAR(50) NOT NULL UNIQUE,
);

CREATE TABLE English_Russian_Words(
    english_id INT,
    russian_id INT,
    FOREIGN KEY (english_id) REFERENCES EnglishWords(id) ON DELETE CASCADE ON UPDATE CASCADE,
    FOREIGN KEY (russian_id) REFERENCES RussianWords(id) ON DELETE CASCADE ON UPDATE CASCADE,
    PRIMARY KEY(english_id, russian_id),
);