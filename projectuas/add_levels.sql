-- SQL Script to add TEEN and EXPERT level questions (Single Words, but Difficult)
USE db_vr;

-- Add TEEN level questions (Difficult single words)
INSERT INTO questions (text, level) VALUES 
('independent', 'teen'),
('knowledge', 'teen'),
('communication', 'teen'),
('beautiful', 'teen'),
('technology', 'teen'),
('architecture', 'teen'),
('environment', 'teen'),
('algorithm', 'teen'),
('significant', 'teen'),
('opportunity', 'teen');

-- Add EXPERT level questions (Very complex single words)
INSERT INTO questions (text, level) VALUES 
('phenomenon', 'expert'),
('conscientious', 'expert'),
('deterioration', 'expert'),
('anemone', 'expert'),
('pronunciation', 'expert'),
('entrepreneur', 'expert'),
('synchronicity', 'expert'),
('metamorphosis', 'expert'),
('procrastinate', 'expert'),
('vocabulary', 'expert');
