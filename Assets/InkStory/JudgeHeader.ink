
VAR inn=0
VAR ext=0
VAR lgc=0
VAR spt=0
VAR mrl=0
VAR utc=0
VAR rdb=0
VAR ags=0
//内视值是否在区间[l,r)内，l,r为正整数
EXTERNAL InnIsIn(l,r)
//外感值是否在区间[l,r)内，l,r为正整数
EXTERNAL ExtIsIn(l,r)
//逻辑值是否在区间[l,r)内，l,r为正整数
EXTERNAL LgcIsIn(l,r)
//激情值是否在区间[l,r)内，l,r为正整数
EXTERNAL SptIsIn(l,r)
//道德值是否在区间[l,r)内，l,r为正整数
EXTERNAL MrlIsIn(l,r)
//无忌值是否在区间[l,r)内，l,r为正整数
EXTERNAL UtcIsIn(l,r)
//迂回值是否在区间[l,r)内，l,r为正整数
EXTERNAL RdbIsIn(l,r)
//强势值是否在区间[l,r)内，l,r为正整数
EXTERNAL AgsIsIn(l,r)

//对话是否判定成功
VAR judgeSuccess=false
    