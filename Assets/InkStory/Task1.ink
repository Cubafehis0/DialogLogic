// 接受任务
-> task_acc
INCLUDE JudgeHeader.ink




== task_acc
你接到了黑帮头目的要求，去收这个月的保护费。 #Speaker:旁白
平民区的居住者主要是没有身份和大量金钱的人，他们每天忙碌工作却只够糊口。#Speaker:旁白
这里充斥着暴力和混乱，而这个混乱地区的律令由欧佩克帮和托拉斯家族两个组织进行维持。#Speaker:旁白
前一个是移动世界中最大的暴力组织，而后一个则是新近崛起、臭名昭著的军火贩子。#Speaker:旁白

+ 接受任务。   
-> task_start

// 任务开始
==task_start
你前往这次收取保护费的地方。 #Speaker:旁白
这是一家穷困潦倒的黑户，向你们缴纳保护费才得以在这里生活下去，但保护费也让他们家徒四壁。#Speaker:旁白
当你走到一个小院，准备敲响房门收保护费时，此时一个皱着眉头的年轻女子出现在你面前，她看起来有点瑟缩，甚至是有点害怕和你对视，但她打起精神望向你。#Speaker:旁白

你是来做什么的？        #Speaker:NPC

+ 我是来收保护费的。
~LogicPlus(1)
-> b1
+ 我是来做应该做的事情的。
~StrongPlus(1)
->b1
+ 请问你是那位?
~UnethicPlus(1)
->b2


= b1 
收保护费？#Speaker:NPC
女子看看身后显得有些破破烂烂的屋子。
你和他们是一伙的？ #Speaker:NPC

+ 我是  
->b1_1

+ 我不太清楚你指的是什么
~DetourPlus(-1)
->b1_1

+ 我劝你少管闲事 @SpeechArt:威胁 CanUse:{ChoiceCanUse(0,0,0,-6)}
->b2_1

+ 我和他们不是一伙人，我会妥善处理这件事 @SpeechArt:欺骗 CanUse:{ChoiceCanUse(0,3,-3,3)}
->b2_1


=b1_1
她往后退了一步，但仍然抬头直视你。#Speaker:旁白
你到底想干什么？                #Speaker:NPC

+ [我讨厌被人质问] @SpeechArt:威胁 CanUse:{ChoiceCanUse(0,0,-4,-4)}
你握紧拳头 #Speaker:旁白
我讨厌被人质问，我更习惯用暴力回答问题。#Speaker:主角
->END2

+ 我只是做该做的而已。 @SpeechArt:说服   CanUse:{ChoiceCanUse(-2,0,0,2)}
~ judgeSuccess=TalkJudge(0,0,0,0)
{judgeSuccess==true:
    ->END1
    - else:
    ->END2
    }

+ 我们只是在保护他们免受伤害。  @SpeechArt:欺骗 CanUse:{ChoiceCanUse(0,0,3,3)}

~ judgeSuccess=TalkJudge(0,0,0,0)
{judgeSuccess==true:
    ->END1
    - else:
    ->END2
    }


=b1_2
你以为我不知道吗？你根本就只是来勒索，为了满足你们暴徒的愿望来干这些龌蹉事！#Speaker:NPC
~ PassionPlus(1) 
~ UnethicPlus(1)
~ DetourPlus(1)
+ 闪开，否则我就要动手了 @SpeechArt:威胁 CanUse:{ChoiceCanUse(0,0,0,-3)}
->END2

+ 他们接受我们的保护，没有我们他们早就因为黑户被管理局抓走了。  @SpeechArt:说服  CanUse:{ChoiceCanUse(0,2,3,0)}
->END2

+ 我们会提供帮助保障他们的生活的。@SpeechArt:欺骗 CanUse:{~ChoiceCanUse(0,2,2,2)}
->END2


= b2
我是他们的邻居，我来帮助他们。 #Speaker:NPC

+ 你并不知道发生了什么，这件事与你无关 @SpeechArt:说服 CanUse:{~ChoiceCanUse(0,2,0,-2)}
->END2

+ 我劝你少管闲事，你挡着我的路了 @SpeechArt:威胁 CanUse:{~ChoiceCanUse(0,0,-2,-6)}
->END2

+ 我是来做我应该做的事情
->b1

->END1
然后她走近两步，直视着你的眼睛，像是要从你眼中读出你内心真实所想，问道：
你说的是真的吗？你能保证吗？
* 我不确定，但我会尽力达成。
->END2
* 我目前只知道这笔钱是我们应得的，其他的不归我管，但我可以帮你去找其他人问问。
->END

=b2_1
->END

=END1

->END


=END2
她愤怒的看着你，张开双手挡住了你面前的路。 #Speaker:旁白
你别过来，再往前一步我就要找警署了。       #Speaker:NPC
你耸耸肩，无所谓的摊开手。                  #Speaker:旁白
悉听尊便，但是你没办法阻挡我。              #Speaker:主角
你推开她，走到房门前按响门铃，大声道：#Speaker:旁白
别磨蹭了，快把钱拿出来。            #Speaker:主角
一时间周围悄无声息，只有机器运行时齿轮发出的咔咔声。#Speaker:旁白
再不出来我就进去了！                #Speaker:主角
女子上前死死拉住你的手，试图阻止你进入房子，但你只是哼了一声，随手甩开她，就用脚踢开了房门，径直走了进去。#Speaker:旁白
不久，房内便被你翻了个底朝天，值钱的东西被你洗劫一空，你对到手的东西非常满意。#Speaker:旁白
女子缩在角落里颤抖，但她看着屋内的一片狼藉，坚定了去警署报警的决心。在你走后，她前往警署报警。#Speaker:旁白
->END