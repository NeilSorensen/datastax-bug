CREATE TABLE IF NOT EXISTS sample.events (
	event_creator text,
	event_id timeuuid,
	note text,
	primary key (event_creator, event_id)
);