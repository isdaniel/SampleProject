-- BETWEEN 以及 >= <
-- 有時因需要特定區間條件會使用到Between (Between實際上是使用 >= 及 <=)
-- 需以實際需求使用但不建議在WHERE條件上對欄位作資料類型轉換或運算
-- 常有需要針對訂單取日期做判斷 且常有人會把日期組成 yyyy-mm-dd 23:59:59 認為這就是當天全部訂單

--取日期欄位
--取當下系統時間
SELECT	GETDATE()

--取當下系統時間的前一天
SELECT	GETDATE()-1

--常用方法
SELECT	CONVERT(VARCHAR(10), GETDATE(),23)
SELECT	CONVERT(VARCHAR(10), GETDATE()-1,23)

-- 建議用法
SELECT	DATEADD(DAY,0,DATEDIFF(DAY,0,GETDATE()))
SELECT	DATEADD(DAY,-1,DATEDIFF(DAY,0,GETDATE()))


--建立測試用暫存表
CREATE TABLE #T (name VARCHAR(10),CreateDate DATETIME)

--寫入測試資料
INSERT INTO #T VALUES
('劉備','2017-03-02 00:00:00.000'),
('關羽','2017-03-02 16:32:51.133'),
('張飛','2017-03-04 12:01:38.217'),
('趙雲','2017-03-05 19:24:10.651'),
('黃忠','2017-03-09 08:19:22.913'),
('馬超','2017-03-13 00:00:00.000'),
('曹操','2017-03-13 00:00:00.397'),
('諸葛亮','2017-03-13 23:01:09.197')

--1. 使用不轉換欄位及VARCHAR(10)的方式去查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	[CreateDate] BETWEEN '2017-03-02' AND '2017-03-13'
--實際取出6筆資料 因Between 等同於 >= + <=
--包含'2017-03-13 00:00:00.000'的資料

--2. 使用轉換CreateDate為DATE型態並使用VARCHAR(10)的方式查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	CAST([CreateDate] AS DATE) BETWEEN '2017-03-02' AND '2017-03-13'
--實際取出8筆資料 因Between 等同於 >= + <=
--且第8筆資料已被轉換為'2017-03-13'
--包含'2017-03-13 00:00:00'及以後的的資料

--3. 直接使用與欄位相同的日期格式查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	[CreateDate] >= '2017-03-02 00:00:00.000'
	AND [CreateDate] < '2017-03-13 00:00:00.000'

--實際取出5筆資料
--不包含'2017-03-13 00:00:00.000'的資料

--4. 直接使用與欄位相同的日期格式及Between查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	[CreateDate] BETWEEN '2017-03-02' AND '2017-03-13'
--實際取出6筆資料
--不包含'2017-03-13 00:00:00.397'及以後的的資料


--5. 實際驗證Credit中o_auth的資料表查詢
SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] >= '2017-03-02 00:00:00.000'
	AND [dtymd] < '2017-03-13 00:00:00.000'

SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] >= '2017-03-02 00:00:00.000'
	AND [dtymd] <= '2017-03-12 23:59:59.997'

SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] > '2017-03-01 23:59:59.997'
	AND [dtymd] <= '2017-03-12 23:59:59.997'

SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] Between '2017-03-02' AND '2017-03-13'

--執行計畫的成本 4次查詢每次最高應為25%
-- >= 加上 < 約佔18%%
-- >= 加上 <= 約佔32%
-- > 加上 <= 約佔18%
-- Between 約佔18%
-- 且Between有可能會造成撈取到錯誤資料，因為會包含 yyyy-mm-dd 00:00:00
-- BETWEEN 以及 >= <
-- 有時因需要特定區間條件會使用到Between (Between實際上是使用 >= 及 <=)
-- 需以實際需求使用但不建議在WHERE條件上對欄位作資料類型轉換或運算
-- 常有需要針對訂單取日期做判斷 且常有人會把日期組成 yyyy-mm-dd 23:59:59 認為這就是當天全部訂單

--取日期欄位
--取當下系統時間
SELECT	GETDATE()

--取當下系統時間的前一天
SELECT	GETDATE()-1

--常用方法
SELECT	CONVERT(VARCHAR(10), GETDATE(),23)
SELECT	CONVERT(VARCHAR(10), GETDATE()-1,23)

-- 建議用法
SELECT	DATEADD(DAY,0,DATEDIFF(DAY,0,GETDATE()))
SELECT	DATEADD(DAY,-1,DATEDIFF(DAY,0,GETDATE()))


--建立測試用暫存表
CREATE TABLE #T (name VARCHAR(10),CreateDate DATETIME)

--寫入測試資料
INSERT INTO #T VALUES
('劉備','2017-03-02 00:00:00.000'),
('關羽','2017-03-02 16:32:51.133'),
('張飛','2017-03-04 12:01:38.217'),
('趙雲','2017-03-05 19:24:10.651'),
('黃忠','2017-03-09 08:19:22.913'),
('馬超','2017-03-13 00:00:00.000'),
('曹操','2017-03-13 00:00:00.397'),
('諸葛亮','2017-03-13 23:01:09.197')

--1. 使用不轉換欄位及VARCHAR(10)的方式去查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	[CreateDate] BETWEEN '2017-03-02' AND '2017-03-13'
--實際取出6筆資料 因Between 等同於 >= + <=
--包含'2017-03-13 00:00:00.000'的資料

--2. 使用轉換CreateDate為DATE型態並使用VARCHAR(10)的方式查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	CAST([CreateDate] AS DATE) BETWEEN '2017-03-02' AND '2017-03-13'
--實際取出7筆資料 因Between 等同於 >= + <=
--且第七筆資料已被轉換為'2017-03-13'
--包含'2017-03-13 00:00:00'的資料

--3. 直接使用與欄位相同的日期格式查詢
SELECT	name,
		CreateDate
FROM #T
WHERE
	[CreateDate] >= '2017-03-02 00:00:00.000'
	AND [CreateDate] < '2017-03-13 00:00:00.000'

--實際取出5筆資料
--不包含'2017-03-13 00:00:00.000'的資料


--4. 實際驗證Credit中o_auth的資料表查詢
SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] >= '2017-03-02 00:00:00.000'
	AND [dtymd] < '2017-03-13 00:00:00.000'

SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] >= '2017-03-02 00:00:00.000'
	AND [dtymd] <= '2017-03-12 23:59:59.997'

SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] > '2017-03-01 23:59:59.997'
	AND [dtymd] <= '2017-03-12 23:59:59.997'

SELECT	TradeID,
		[dtymd]
FROM [AllPay_Credit].[dbo].[o_auth]
WHERE
	[dtymd] Between '2017-03-02' AND '2017-03-13'

--執行計畫的成本 4次查詢每次最高應為25%
-- >= 加上 < 約佔18%%
-- >= 加上 <= 約佔32%
-- > 加上 <= 約佔18%
-- Between 約佔18%
-- 且Between有可能會造成撈取到錯誤資料，因為會包含 yyyy-mm-dd 00:00:00
--7/12 註記 因有進行SQL調校過 故目前均為25%


TRUNCATE TABLE #T
DROP TABLE #T

TRUNCATE TABLE #T
DROP TABLE #T