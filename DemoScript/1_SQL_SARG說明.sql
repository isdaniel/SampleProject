--以符合SARG Statement進行撰寫
-- 符合的如下
-- <、>、=、<=、>=、LIKE(視%所在位置，前面有%讓DB engine選擇不走INDEX) 和BETWEEN
-- 不符合的如下
-- <>、!<、!>、NOT、NOT IN、NOT EXISTS和NOT LIKE、不對欄位作運算、不對欄位使用函數等

-- DB Engine不會很聰明地幫各位選到最好的執行方法
-- SQL的撰寫以越簡單越好 別複雜化

