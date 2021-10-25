//内视值增加a
EXTERNAL InsidePlus(a)
//外感值增加a
EXTERNAL OutsidePlus(a)
//逻辑值增加a
EXTERNAL LogicPlus(a)
//激情值增加a
EXTERNAL PassionPlus(a)
//道德值增加a
EXTERNAL MoralPlus(a)
//无忌值增加a
EXTERNAL UnethicPlus(a)
//迂回值增加a
EXTERNAL DetourPlus(a)
//强势值增加a
EXTERNAL StrongPlus(a)


//内视值设置为a
EXTERNAL InsideSet(a)
//外感值设置为a
EXTERNAL OutsiderSet(a)
//逻辑值设置为a
EXTERNAL LogicSet(a)
//激情值设置为a
EXTERNAL PassionSet(a)
//道德值设置为a
EXTERNAL MoralSet(a)
//无忌值设置为a
EXTERNAL UnethicSet(a)
//迂回值设置为a
EXTERNAL DetourSet(a)
//强势值设置为a
EXTERNAL StrongSet(a)

VAR Inside=0
VAR Outside=0
VAR Logic=0
VAR Passion=0
VAR Moral=0
VAR Unethic=0
VAR Detour=0
VAR Strong=0
//内视值是否在区间[l,r)内，l,r为正整数
EXTERNAL InsideIsIn(l,r)
//外感值是否在区间[l,r)内，l,r为正整数
EXTERNAL OutsideIsIn(l,r)
//逻辑值是否在区间[l,r)内，l,r为正整数
EXTERNAL LogicIsIn(l,r)
//激情值是否在区间[l,r)内，l,r为正整数
EXTERNAL PassionIsIn(l,r)
//道德值是否在区间[l,r)内，l,r为正整数
EXTERNAL MoralIsIn(l,r)
//无忌值是否在区间[l,r)内，l,r为正整数
EXTERNAL UnethicIsIn(l,r)
//迂回值是否在区间[l,r)内，l,r为正整数
EXTERNAL DetourIsIn(l,r)
//强势值是否在区间[l,r)内，l,r为正整数
EXTERNAL StrongIsIn(l,r)

//选项是否可选判定，i,l,m,d为内视、逻辑、道德、迂回的门槛值，如果为负值则表示是其对应的属性值门槛值
EXTERNAL ChoiceCanUse(i,l,m,d)

//对话判定，i,l,m,d为内视、逻辑、道德、迂回的门槛值，如果为负值则表示是其对应的属性值门槛值
EXTERNAL TalkJudge(i,l,m,d)

//对话是否判定成功
VAR judgeSuccess=false
//选项是否可用
VAR canUSe=false