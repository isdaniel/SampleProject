-- SELECT語法養成習慣性加上 WITH (NOLOCK)
EX: 
SELECT     A.TradeID
FROM dbo.Payment_TradeDetail_AliPay AS A
     INNER JOIN dbo.Payment_TradeNo AS B ON A.TradeID = B.TradeID
可改為

SELECT     A.TradeID
FROM dbo.Payment_TradeDetail_AliPay AS A WITH (NOLOCK)
     INNER JOIN dbo.Payment_TradeNo AS B WITH (NOLOCK) ON A.TradeID = B.TradeID

-- WITH (NOLOCK)會讓DB Engine不管該Table/Row是否包含有尚未commit的資料，一律都會回傳當下看到的值
