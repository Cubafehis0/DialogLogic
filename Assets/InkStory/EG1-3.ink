->task_start
INCLUDE JudgeHeader.ink



=== task_start === 
夜深人静之时，这座蒸汽推动的城市也停下了轰鸣，陷入了短暂的沉睡，这时，就是属于这些黑夜里的“老鼠们”的时间了。而你也是他们当中的一员。	#Speaker:Dialogue 


你心情愉快地结束了与马仔们的酒会，老大对你能顺利将保护费收齐感到很满意，在酒吧频频与你举杯庆祝。	#Speaker:Dialogue 


待到酒终人散，你收紧大衣，离开了灯红酒绿的环轨酒吧，带着一身酒气走进了夜幕里，黑暗是你最好的保护色。	#Speaker:Dialogue 


正当你走进一个小巷时，前面传来的一声喊叫引起了你的警惕，你停下了脚步，警惕着周围的情况，可是并无异状，喊叫声也仿佛从来没有发生，一声过后便再无动静。你这才继续前进，不过仍然放慢了脚步。	#Speaker:Dialogue 


这时，你来到了一个转角处，这里只有昏暗的路灯给这团黑色的幕布带来了些许亮色，你停下脚步，警觉地打量着四周，这是一个伏击的绝佳位置，必须小心谨慎。	#Speaker:Dialogue 


霎时间，一个人影从阴影中窜了出来，拿着枪对着你，你下意识摸了摸口袋，却发现手枪因为今天的酒会而落在家里了。你只好硬着头皮向前走去。	#Speaker:Dialogue 


借助路灯的余光，你发现那人似乎是一个便衣警探，心中越发觉得不妙，开始暗暗思索脱身之道，你想到因为今晚的酒会，身上充斥着刺鼻的酒气，觉得装成醉汉是可行的，于是摇摇晃晃的走上前去。	#Speaker:Dialogue 



“你是什么人？从哪来的？去干什么的？”那人问道，“我是一名警探，最近在调查黑帮，请你配合我。”	#Speaker:NPC 


你心想：撞到枪口上了，得想个办法糊弄过去	#Speaker:NPC 


+ 我和朋友去酒吧借酒浇愁了	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
我和朋友去酒吧借酒浇愁了	#Speaker:Player 
-> b_first

+ 嘴里嘟嘟哝哝地说了些散乱的词句，只能听清酒吧，喝酒之类的字眼	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,-1,-1)  
嘴里嘟嘟哝哝地说了些散乱的词句，只能听清酒吧，喝酒之类的字眼	#Speaker:Player 
-> b_first

+ 发酒疯，让他不要管你	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,1,1,-1)  
发酒疯，让他不要管你	#Speaker:Player 
-> b_first





=== b_first === 
他闻到你身上的酒气，皱了皱眉头，继续问道：“你们去了那个酒吧？”	#Speaker:NPC 


+ 环轨酒吧	   @SpeechArt:Pursuade  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
环轨酒吧	#Speaker:Player 
-> b_frist_one

+ 我记不太清了	   @SpeechArt:Threaten  @Speaker:Player    @StateChange:(0,0,-1,-1,-1)  
我记不太清了	#Speaker:Player 
-> b_frist_one

+ 朋克酒吧	   @SpeechArt:Cheat  @Speaker:Player    @StateChange:(0,0,1,1,-1)  
朋克酒吧	#Speaker:Player 
-> b_frist_one





=== b_frist_one === 
你偷偷看了眼他的神情，发现他并没有怀疑你。他又继续问道：“你们是不是喝了格瓦斯啊？我挺喜欢这种酒的。”	#Speaker:NPC 


+ 你装作酒醉的样子含糊地回答道：“应该喝了吧，我们什么酒都试了一点。”	   @SpeechArt:Pursuade  @Speaker:Player    @StateChange:(0,0,0,1,-1)  
	#Speaker:Player 
-> b_frist_two




=== b_frist_two === 
他不依不饶：“你觉得味道怎么样？”	#Speaker:NPC 


+ 你含混地回答道：“记不清了，我们喝了不少酒。”	   @SpeechArt:Pursuade  @Speaker:Player    @StateChange:(0,0,0,1,-1)  
	#Speaker:Player 
-> E_one





=== E_one === 
“你并不是什么醉汉吧，”他直视着你的双眼，“环轨酒吧里并没有叫格瓦斯的酒，你是装出来的。”同时他翻开你的袖口，指着那个烙印说道：“这就是你们黑帮的标记吧，你承不承认？”	#Speaker:Dialogue 


你发觉身份已经暴露，转身就想跑，后面响起的枪声使你停下了脚步。那警探说道：“我建议你还是不要轻举妄动，警察听到枪声已经往这边赶来了，还是乖乖和我回警局喝茶吧。”你无奈的举起手来，示意放弃抵抗，一边心想：“可惜没有枪，不然有你好看的。”随后赶来的警察将你带回了警局，关押在牢房里。	#Speaker:Dialogue 

-> END




