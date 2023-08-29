﻿ALTER DATABASE database1  
SET CHANGE_TRACKING = ON  
(CHANGE_RETENTION = 1 DAYS, AUTO_CLEANUP = ON)  

ALTER TABLE Table1  
ENABLE CHANGE_TRACKING  
WITH (TRACK_COLUMNS_UPDATED = ON) 