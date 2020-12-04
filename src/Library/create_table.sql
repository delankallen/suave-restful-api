-- SQLite

DROP TABLE IF EXISTS payload_batch;
DROP TABLE IF EXISTS payloads;

CREATE TABLE `payload_batch` (
  id INTEGER PRIMARY KEY,
  batch_id TEXT,
  creation_date TEXT
);

CREATE TABLE `payloads` (
  id INTEGER PRIMARY KEY,
  batch_id TEXT,
  type_object INTEGER,
  type_value TEXT
);

INSERT INTO payload_batch(batch_id, creation_date) VALUES('158b5a56-fc24-4ecf-bd86-5ea045334cc6', DATETIME('now'));
INSERT INTO payload_batch(batch_id, creation_date) VALUES('158b5a56-fc24-4ecf-bd86-5ea045334ddscc6', DATETIME('now'));
INSERT INTO payloads(batch_id, type_object, type_value) VALUES('158b5a56-fc24-4ecf-bd86-5ea0sdfd45334ddscc6', 2, '1796');

SELECT * FROM payload_batch;
SELECT * FROM payloads;