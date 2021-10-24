EXTERNAL logicPlus(a)
EXTERNAL moralPlus(a)
EXTERNAL strongPlus(a)
EXTERNAL choiceCanUse(i,l,m,d)

# 接受任务
-> task_acc
== task_acc
你接到了，黑帮头目的要求，去收这个月的保护费。 #Speaker:旁白
平民区的居住者主要是没有身份和大量金钱的人，他们每天忙碌工作却只够糊口。#Speaker:旁白
这里充斥着暴力和混乱，而这个混乱地区的律令由欧佩克帮和托拉斯家族两个组织进行维持。#Speaker:旁白
前一个是移动世界中最大的暴力组织，而后一个则是新近崛起、臭名昭著的军火贩子。#Speaker:旁白

* 接受任务。        #Speaker:旁白
-> task_start

# 任务开始
==task_start
你前往这次收取保护费的地方。 #Speaker:旁白
这是一家穷困潦倒的黑户，向你们缴纳保护费才得以在这里生活下去，但保护费也让他们家徒四壁。#Speaker:旁白
当你走到一个小院，准备敲响房门收保护费时，此时一个皱着眉头的年轻女子出现在你面前，她看起来有点瑟缩，甚至是有点害怕和你对视，但她打起精神望向你。#Speaker:旁白

你是来做什么的？        #Speaker:NPC

* 我是来收保护费的。
~logicPlus(1)
-> b1
* 我是来做应该做的事情的。
~strongPlus(1)
->b1
* 请问你是那位?
~moralPlus(-1)
->b2


= b1 
收保护费？你和他们是一伙的？ #Speaker:NPC
* 我是  
->b1_1
* 我不太清楚你指的是什么
~strongPlus(-1)
->b1_1
* 我劝你少管闲事 @SpeechArt:威胁 CanUse:{~choiceCanUse(0,0,0,-6)}
->b2_1
* 我和他们不是一伙人，我会妥善处理这件事 @SpeechArt:欺骗 CanUse:{~choiceCanUse(0,3,-3,3)}
->b2_1

=b1_1
*我讨厌被人质问，我更习惯用暴力回答问题。
你握紧拳头 #Speaker:旁白
->END
* 我只是做该做的而已。 

= b2
->END

=b2_1
->END

=END1

->END
=END2

->END
你以为我不知道吗？你根本就只是来勒索，为了满足你们暴徒的愿望来干这些龌蹉事！
~ logicPlus(-1) 
~ moralPlus(-1)
~ strongPlus(-1)
->END