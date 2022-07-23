->task_start
INCLUDE JudgeHeader.ink



=== task_start === 
城市陷入了漆黑的沉睡中，环轨之上的蒸汽列车如长蛇般蜿蜒前行，在熟睡的城市里挪移观察。
你站在街边的一角，嘴里叼着香烟吞云吐雾着。烟草里夹杂着醒神的草药，这即使是深夜也能让他的神智无比清醒，甚至说还有点小兴奋。
带着这微弱的兴奋，手杖轻轻的敲打着地面，根据印象里的韵律，轻快的击打着。
抬起头，望着昏暗的路灯，你的思绪逐渐飘散。
	#Speaker:Dialogue 


尖锐的汽鸣打断了你的思绪，看向一边，炽热的白气从街道附近的井盖里喷发而出，随后在寒冷的晚风之下它们凝结成数不清的水珠飘散，最后弥散成灰白的雾气将整个城市覆盖。	#Speaker:Dialogue 


低头看了看怀表，时间差不多到了，也在这时街道的尽头传来急促的呼吸与奔跑声。 看起来自己要等的人很准时。
于是你丢掉了烟蒂，将手伸进厚重的风衣之下，取出那把你心爱的小手枪。	#Speaker:Dialogue 


脚步声越来越近，直至就在耳边，你压抑住内心的狂跳，屏住呼吸，隐藏好自己的气息，等待最佳的时机	#Speaker:Dialogue 


脚步声突然停了下来，那人似乎意识到了什么，你果断从转角处现身，用手枪指着黑暗中的身影，那人愣了愣，走到昏暗的路灯下现出身形，并不是你蹲守的那个黑帮分子，你尴尬地放下了枪，低声向路人道歉。待那人走后，你摸着枪身，无奈的叹了口气。	#Speaker:Dialogue 


就在这时，脚步声再次响起，你打了个哆嗦，收紧大衣，心又开始狂跳，你是否准备继续堵截此人？	#Speaker:Dialogue 


+ 是，重新隐藏身形，屏气凝神，准备堵截这人	   @SpeechArt:Normal  @Speaker:Player    
	#Speaker:Player 
-> task_start_one

+ 否，情报出现了误差，明晚再来	   @SpeechArt:Normal  @Speaker:Player    
	#Speaker:Player 
-> END





=== task_start_one === 
你毅然选择相信情报的准确性，蹲在阴影处等待着他的到来。	#Speaker:Dialogue 


脚步声逐渐靠近，你从阴影处走出来，将手枪对着那个黑影，那人摇摇晃晃地走到了路灯下，看上去只是一个喝醉酒的酒鬼。	#Speaker:Dialogue 


你不禁再次对自己的判断产生了怀疑，这个人真的是我要找的吗？	#Speaker:Dialogue 


+ 看上去这就是个醉汉，今天算是白忙活了	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
看上去这就是个醉汉，今天算是白忙活了	#Speaker:Player 
-> END

+ 别急着下判断，再观察一阵子	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,0,-1,-1)  
别急着下判断，再观察一阵子	#Speaker:Player 
-> b_first

+ 这家伙很可疑，他现在摇摇晃晃的，那为什么之前的脚步声那么整齐呢？	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Speaker:Player    @StateChange:(0,0,0,1,-1)  
这家伙很可疑，他现在摇摇晃晃的，那为什么之前的脚步声那么整齐呢？	#Speaker:Player 
{judgeSuccess==true:
    ->b_first
    - else:
    ->task_start_one    
}





=== b_first === 
你走上前去截住了那个醉汉，他浑身酒气，嘴里含含混混地嘟哝着什么，似乎没什么异样	#Speaker:NPC #StateChange:(0,1,0,0,-1)  


+ 感觉没有异常，所以真是情报出现了问题？	   @SpeechArt:Pursuade  @Speaker:Player    
感觉没有异常，所以真是情报出现了问题？	#Speaker:Player 
-> b_frist_one

+ 冷静一下，先询问他两句话再说	   @SpeechArt:Pursuade  @Speaker:Player    
冷静一下，先询问他两句话再说	#Speaker:Player 
-> b_frist_one

+ 为什么这人一直在偷瞄我的神情，这人真的是醉汉吗？	   @SpeechArt:Pursuade  @Speaker:Player    
为什么这人一直在偷瞄我的神情，这人真的是醉汉吗？	#Speaker:Player 
-> b_frist_one





=== b_frist_one === 
你出示了自己的身份证件，表明了自己的来意，询问他是从哪里来的，并且想知道他为什么醉成这个样子？那个人靠在路边的栏杆上，双眼迷蒙地回答道：“我和朋友在酒吧借酒浇愁。”你再询问道：“是在环轨酒吧吗？”他说：“啊对对对，就是哪儿。”你又问道：“喝了“格瓦斯”吗？“他摇了摇头，又点了点头，说：“我们什么都喝了点，应该是喝了的。”	#Speaker:NPC #StateChange:(0,1,0,0,-1)  


+ 这个人就是个单纯的醉鬼，放他过去吧	   @SpeechArt:Pursuade  @Speaker:Player    
这个人就是个单纯的醉鬼，放他过去吧	#Speaker:Player 
-> END

+ 格瓦斯？我记得环轨酒吧不供应这种酒，也可能是我记错了	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Success:5  @Fail:15  @Speaker:Player    
格瓦斯？我记得环轨酒吧不供应这种酒，也可能是我记错了	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
    ->b_frist_one    
}

+ 环轨酒吧里供应的是格瓦拉品牌的粗麦酒，不是什么格瓦斯，他就是那个我要找的人	   @SpeechArt:Pursuade  @CanUse:(0,6,0,0)    @Success:5  @Fail:15  @Speaker:Player    
环轨酒吧里供应的是格瓦拉品牌的粗麦酒，不是什么格瓦斯，他就是那个我要找的人	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
    ->b_frist_one    
}




=== E_one === 
“你并不是什么醉汉吧，”你直视着他的双眼，“环轨酒吧里并没有叫格瓦斯的酒，你是装出来的。”同时你翻开他的袖口，指着那个烙印说道：“这就是你们黑帮的标记吧，你承不承认？”	#Speaker:NPC 


他转身欲跑，你向天开了一枪，说道：“我建议你还是不要轻举妄动，警察听到枪声已经往这边赶来了，还是乖乖和我回警局喝茶吧。”	#Speaker:Dialogue 


他举起手来，示意放弃抵抗，被赶来的警察带回了警局	#Speaker:Dialogue 

-> END



