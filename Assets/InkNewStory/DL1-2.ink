->task_start
INCLUDE JudgeHeader.ink



=== task_start === 
接受委托后，你来到住宅区，放眼望去，这里遍布的是破破烂烂的房屋，还有随处可见的小混混，尖锐的轰鸣声不断撞击着你的耳膜。你撇撇嘴，收紧大衣，仿佛不想沾染这里的空气，心想：这是什么鬼地方，尽早办完事缩回躺椅吧	#Speaker:Dialogue 


你从口袋里拿出女委托人写给你的地址，又拿出地图确认了位置，扫视四周确定方位径直走向了目的地。	#Speaker:Dialogue 


在前往地点的路上，你走进了一条幽深的小巷，此时前面跳出几个拿着武器的小混混拦住了你，后面也有人堵住了你的退路。	#Speaker:Dialogue 


他们恶狠狠的盯着你，挥舞着手中的武器，威胁你：“要钱还是要命？”你心里想：真倒霉，碰上这种事，我是掏枪威胁他们让个道还是直接和他们火拼呢？	#Speaker:Dialogue 


+ 威胁他们让个道	   @SpeechArt:Pursuade  @Speaker:Player    @StateChange:(0,1,0,0,3)  
	#Speaker:Player 
-> task_start_one

+ 掏枪火拼	   @SpeechArt:Threaten  @Speaker:Player    @StateChange:(0,0,0,-1,3)  
	#Speaker:Player 
-> task_start_one

+ 给钱回家	   @SpeechArt:Cheat  @Speaker:Player    
	#Speaker:Player 
-> END




=== task_start_one === 
你到了目的地，敲了敲房主的门，无人应答，你只好选择先去询问一下邻居，看看能不能得到一些线索	#Speaker:Dialogue 


你决定先从旁边的住户开始。你敲响了旁边的一间房门，门开了，A探出身子，警惕地看着你，问道：“你是谁？你是来干什么的？”	#Speaker:NPC 


+ 我是受人委托来调查黑帮的，想找你询问情况	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,2)  
我是受人委托来调查黑帮的，想找你询问情况	#Speaker:Player 
-> b_first

+ 你好，我是来帮你们调查黑帮的，能给我提供一些线索吗？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,-1,2)  
你好，我是来帮你们调查黑帮的，能给我提供一些线索吗？	#Speaker:Player 
-> b_first

+ 我是调查黑帮的侦探，麻烦你配合我提供线索	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,1,1,2)  
我是调查黑帮的侦探，麻烦你配合我提供线索	#Speaker:Player 
-> b_first





=== b_first === 
A看了看你：“我这确实经常有黑帮出没，请问你需要什么样的线索你？”	#Speaker:NPC 


+ 能给我提供关于他们的显著特征和出没时间的线索吗？	   @SpeechArt:Normal  @Speaker:Player    
能给我提供关于他们的显著特征和出没时间的线索吗？	#Speaker:Player 
-> b_first_one

+ 想要整治黑帮，我需要他们的外貌特征来认出他们，需要他们的出没时间来蹲守他们，还需要你配合我提供更多信息	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Success:5  @Fail:5  @Speaker:Player    
想要整治黑帮，我需要他们的外貌特征来认出他们，需要他们的出没时间来蹲守他们，还需要你配合我提供更多信息	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_one
    - else:
    ->b_first    
}

+ 把你知道的信息都交给我，不然黑帮知道了你也没好果子吃	   @SpeechArt:Threaten  @CanUse:(0,0,0,-5)    @Success:5  @Fail:5  @Speaker:Player    
把你知道的信息都交给我，不然黑帮知道了你也没好果子吃	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_one
    - else:
    ->b_first    
}






=== b_first_one === 
进屋以后，A向你提供了部分关于黑帮特征的信息，但是对黑帮的出没时间讳莫如深	#Speaker:NPC 


+ 你刚刚说黑帮时常来这里，那你肯定清楚他们出没的时间，我建议你还是告诉我比较好，把这些祸害早点除掉对你们对我都好	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:10  @Fail:10  @Speaker:Player    
你刚刚说黑帮时常来这里，那你肯定清楚他们出没的时间，我建议你还是告诉我比较好，把这些祸害早点除掉对你们对我都好	#Speaker:Player 
{judgeSuccess==true:
    ->talk_two
    - else:
    ->b_first_one    
}

+ 你绝对不止知道这些信息，我奉劝你还是全部告诉我好，否则下次来的就是警察了	   @SpeechArt:Threaten  @CanUse:(0,-4,0,-4)    @Success:10  @Fail:10  @Speaker:Player    
你绝对不止知道这些信息，我奉劝你还是全部告诉我好，否则下次来的就是警察了	#Speaker:Player 
{judgeSuccess==true:
    ->talk_one
    - else:
    ->b_first_one    
}




=== talk_one === 
A被你吓得缩在椅子里，为求自保将线索都告诉了你	#Speaker:NPC 

-> b_first_two



=== talk_two === 
在你的好言劝诱下，A还是把线索告诉了你	#Speaker:NPC 

-> b_first_two




=== b_first_two === 
你敲了敲另一位住户的房门，出来的是一个彪形大汉，兜里鼓鼓的，隐约能看出手枪的轮廓。他粗声粗气地问道：“你小子是来干什么的？”	#Speaker:NPC #StateChange:(0,0,0,1,2)  


+ 我是受人委托来调查黑帮的，想找你询问情况	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,2)  
我是受人委托来调查黑帮的，想找你询问情况	#Speaker:Player 
-> b_first_three

+ 你好，我是来帮你们调查黑帮的，能给我提供一些线索吗？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,-1,2)  
你好，我是来帮你们调查黑帮的，能给我提供一些线索吗？	#Speaker:Player 
-> b_first_three

+ 我是调查黑帮的侦探，麻烦你配合我提供线索	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,1,1,2)  
我是调查黑帮的侦探，麻烦你配合我提供线索	#Speaker:Player 
-> b_first_three





=== b_first_three === 
他不耐烦地摆了摆手，粗鲁地说：“滚开，我不知道这种东西。”	#Speaker:NPC 


+ 你走上前，用锐利眼神直视他，说道：“别把这不当回事，那天黑帮上门寻仇，你以为一把枪能保得住你的命？”	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:10  @Speaker:Player    @StateChange:(0,0,1,0,-1)  
你走上前，用锐利眼神直视他，说道：“别把这不当回事，那天黑帮上门寻仇，你以为一把枪能保得住你的命？”	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_four
    - else:
    ->b_two    
}

+ 你掏出枪，当着他的面解开了保险，又放回大衣口袋：“朋友，现在我们能好好谈下了吗？”	   @SpeechArt:Threaten  @CanUse:(0,-2,0,-3)    @Success:5  @Fail:10  @Speaker:Player    
你掏出枪，当着他的面解开了保险，又放回大衣口袋：“朋友，现在我们能好好谈下了吗？”	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_four
    - else:
    ->b_two    
}





=== b_first_four === 
B将你挡在门口，遮住了你窥探的视线，无所谓地说：“哼，我会怕他们？”	#Speaker:NPC #StateChange:(0,0,-5,0,-1)  



+ 不止这些吧，为你的家人孩子想想，黑帮一天不除，他们就一天得不到安全保障，就是为了他们你也应该协助我铲除这些祸害	   @SpeechArt:Pursuade  @CanUse:(0,6,0,0)    @Success:10  @Fail:15  @Speaker:Player    
不止这些吧，为你的家人孩子想想，黑帮一天不除，他们就一天得不到安全保障，就是为了他们你也应该协助我铲除这些祸害	#Speaker:Player 
{judgeSuccess==true:
    ->talk_three
    - else:
    ->b_first_four    
}

+ 你把手伸进口袋，握紧了枪身，恶狠狠地说：“希望你清楚被警察知道持枪的后果，不想被我检举的话就把你知道的东西全部告诉我。”	   @SpeechArt:Threaten  @CanUse:(0,0,0,-6)    @Success:10  @Fail:15  @Speaker:Player    
你把手伸进口袋，握紧了枪身，恶狠狠地说：“希望你清楚被警察知道持枪的后果，不想被我检举的话就把你知道的东西全部告诉我。”	#Speaker:Player 
{judgeSuccess==true:
    ->talk_four
    - else:
    ->b_first_four    
}





=== talk_three === 
B迟疑了一会：“你能保证不泄露我的身份吗？”得到了你的保证以后，他才放你进屋和你详谈。	#Speaker:Dialogue 

-> E_one



=== talk_four === 
你的行为激怒了他，他掏出手枪，怒吼道：“你小子算个什么东西，还敢威胁老子？给我滚！”你们两个不欢而散。	#Speaker:Dialogue 

-> b_two




=== b_two === 
你敲响了本行最后一个住户的房门，开门的人躲在门后，没有露面，只有声音能判断是男性。他问道：“你是谁？你来干什么？”	#Speaker:NPC 


+ 我是受人委托来调查黑帮的，想找你询问情况	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,2)  
我是受人委托来调查黑帮的，想找你询问情况	#Speaker:Player 
-> b_two_one

+ 你好，我是来帮你们调查黑帮的，能给我提供一些线索吗？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,-1,2)  
你好，我是来帮你们调查黑帮的，能给我提供一些线索吗？	#Speaker:Player 
-> b_two_one

+ 我是调查黑帮的侦探，麻烦你配合我提供线索	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,1,1,2)  
我是调查黑帮的侦探，麻烦你配合我提供线索	#Speaker:Player 
-> b_two_one





=== b_two_one === 
他说道：“我不知道，请你去找别人吧。”说完他就把门关上了。	#Speaker:NPC 


+ 再次敲门询问	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Success:5  @Fail:10  @Speaker:Player    
再次敲门询问	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_two
    - else:
    ->b_two_one    
}

+ 重重地叩击房门	   @SpeechArt:Pursuade  @CanUse:(0,3,0,0)    @Success:5  @Fail:10  @Speaker:Player    
重重地叩击房门	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_two
    - else:
    ->b_two_one    
}

+ 转身离开	   @SpeechArt:Threaten  @CanUse:(0,-2,0,-5)    @Success:5  @Fail:10  @Speaker:Player    
转身离开	#Speaker:Player 
{judgeSuccess==true:
    ->END
    - else:
    ->b_two_one    
}




=== b_two_two === 
门开了，他不耐烦地问道：“都说了不知道，还有什么事啊？”	#Speaker:NPC 


+ 你真的不知道吗？你的线索对我很重要。	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Success:5  @Fail:5  @Speaker:Player    
你真的不知道吗？你的线索对我很重要。	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
    ->E_two    
}

+ 我奉劝你不要不识好歹，这次是我下次就是警察了。	   @SpeechArt:Threaten  @CanUse:(0,0,0,-4)    @Success:5  @Fail:5  @Speaker:Player    
我奉劝你不要不识好歹，这次是我下次就是警察了。	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
    ->E_two    
}




=== b_two_three === 
“那你想知道什么？先提前说了，关于黑帮我只知道一点点线索。“	#Speaker:NPC 


+ 那把你知道的都告诉我就好	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Success:5  @Fail:10  @Speaker:Player    
那把你知道的都告诉我就好	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_four
    - else:
    ->b_two_three    
}

+ 我觉得你肯定有所隐瞒	   @SpeechArt:Threaten  @CanUse:(0,3,0,-2)    @Success:5  @Fail:10  @Speaker:Player    
我觉得你肯定有所隐瞒	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_four
    - else:
    ->b_two_three    
}




=== b_two_four === 
“那你觉得我知道什么呢？”他说道。	#Speaker:NPC 


+ 我希望你能配合我，毕竟黑帮指不定那天就找上你了，这个后果自负。	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:10  @Speaker:Player    
我希望你能配合我，毕竟黑帮指不定那天就找上你了，这个后果自负。	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_five
    - else:
    ->b_two_four    
}

+ 我不需要知道，只需要让你告诉我就行了	   @SpeechArt:Threaten  @CanUse:(0,-2,0,-4)    @Success:5  @Fail:10  @Speaker:Player    
我不需要知道，只需要让你告诉我就行了	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_five
    - else:
    ->b_two_four    
}




=== b_two_five === 
你在威胁我？	#Speaker:NPC 


+ 我不是，我只是在阐述事实	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:15  @Speaker:Player    
我不是，我只是在阐述事实	#Speaker:Player 
{judgeSuccess==true:
    ->talk_six
    - else:
    ->talk_five    
}

+ 你掏出手枪，“我觉得你还是老实交代吧，不然我不保证你的安全。'	   @SpeechArt:Threaten  @CanUse:(0,-3,0,-4)    @Success:5  @Fail:15  @Speaker:Player    
你掏出手枪，“我觉得你还是老实交代吧，不然我不保证你的安全。'	#Speaker:Player 
{judgeSuccess==true:
    ->talk_six
    - else:
    ->talk_five    
}




=== talk_five === 
尽管你好言相劝，但是他拒不配合，你只拿到了很少的信息	#Speaker:NPC 

-> E_two



=== talk_six === 
他被你吓住了，偷偷塞了张纸条给你，纸条上有一些你所需要的信息	#Speaker:NPC 

-> E_one





=== E_one === 
你将自己得到的信息记在了本子上，满意地回到了住处。缩在熟悉的躺椅里，你照着搜集来的线索，推断出他惯常的行动路线，准备在晚上去他的必经之路上蹲守他	#Speaker:NPC 

-> END





=== E_two === 
你将自己得到的信息记在了本子上，但是总觉得缺了很多信息，你决定下次再去一次。	#Speaker:NPC 

-> END









