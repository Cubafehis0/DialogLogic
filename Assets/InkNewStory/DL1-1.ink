->task_start
INCLUDE JudgeHeader.ink



=== task_start === 
在某个阴雨天，正在休假的你百无聊赖的坐在躺椅里，随手翻看着手中的日报，桌上摊满了杂乱的书籍和剪报	#Speaker:Dialogue 


你看着窗外，叹气道：“下雨天真是太无趣了，因为太容易留下线索，都没什么犯罪了。”这时，门铃响了，你看向门口，心里开始考虑：	#Speaker:Dialogue 


+ 开门，这种下雨天还来拜访的委托人，事件必然紧急。	   @SpeechArt:Pursuade  @Speaker:Player    
	#Speaker:Player 
	-> task_start_one

+ 拒绝开门，我需要休息，身体情况不允许我再出门办案	   @SpeechArt:Threaten  @Speaker:Player    
	#Speaker:Player 
	-> END




=== task_start_one === 
你打开门，进来的是一位年轻的女士，她将淋湿的雨伞放在门口，径直走了进来	#Speaker:Dialogue 


你坐在躺椅里，在想应该用什么态度来对待她	#Speaker:Dialogue 


+ 请问你来找我有事吗？	   @SpeechArt:Pursuade  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
	#Speaker:Player 
	-> task_start_two

+ 不好意思，我时间有限，能麻烦你快点吗？	   @SpeechArt:Threaten  @Speaker:Player    @StateChange:(0,0,1,0,-1)  
	#Speaker:Player 
	-> task_start_two





=== task_start_two === 
我来这里是有委托来求助你。	#Speaker:NPC 


+ 请问是什么委托？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
请问是什么委托？	#Speaker:Player 
-> b_first

+ 我有什么能帮得上您的吗？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,1,0,-1)  
我有什么能帮得上您的吗？	#Speaker:Player 
-> b_first

+ 能不能请你快速讲一下？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,0,-1,-1)  
能不能请你快速讲一下？	#Speaker:Player 
-> b_first





=== b_first === 
年轻女子坐到你对面的椅子上，描述了一下事件的详情	#Speaker:NPC 


+ 能麻烦你更具体的再描述一下事情吗？	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:5  @Speaker:Player    
能麻烦你更具体的再描述一下事情吗？	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_one
    - else:
      ->b_first    
    }

+ 请问你想委托我做什么呢？	   @SpeechArt:Pursuade  @CanUse:(0,3,0,0)    @Success:5  @Fail:5  @Speaker:Player    
请问你想委托我做什么呢？	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_one
    - else:
      ->b_first    
    }

+ 我对这件事很感兴趣，请务必让我接手	   @SpeechArt:Threaten  @CanUse:(0,-5,0,0)    @Success:5  @Fail:5  @Speaker:Player    
我对这件事很感兴趣，请务必让我接手	#Speaker:Player 
{judgeSuccess==true:
    ->b_two
    - else:
      ->b_first    
    }

+ 这个委托比较麻烦，我不希望你来插手	   @SpeechArt:Threaten  @CanUse:(0,3,0,0)    @Success:5  @Fail:5  @Speaker:Player    
这个委托比较麻烦，我不希望你来插手	#Speaker:Player 
{judgeSuccess==true:
    ->b_two
    - else:
      ->b_first    
    }





=== b_first_one === 
您能完成我的委托吗？	#Speaker:NPC 


+ 我是专业的，你要相信我的实力	   @SpeechArt:Pursuade  @CanUse:(0,3,0,0)    @Success:5  @Fail:10  @Speaker:Player    
我是专业的，你要相信我的实力	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
      ->b_first_one    
    }

+ 请你尊重我的水平	   @SpeechArt:Pursuade  @CanUse:(0,4,0,0)    @Success:5  @Fail:10  @Speaker:Player    
请你尊重我的水平	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
      ->b_first_one    
    }

+ 这得看具体情况	   @SpeechArt:Pursuade  @CanUse:(0,3,0,0)    @Success:5  @Fail:10  @Speaker:Player    
这得看具体情况	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
      ->b_first_one    
    }





=== b_first_two === 
你觉得这个委托人怎么样？	#Speaker:NPC 


+ 看起来很有正义感，我决定尽力帮助她	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:10  @Speaker:Player    
看起来很有正义感，我决定尽力帮助她	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_three
    - else:
      ->b_first_two    
    }

+ 看起来挺好糊弄的，可以随便做点调查就交差	   @SpeechArt:Pursuade  @CanUse:(0,3,0,0)    @Success:5  @Fail:10  @Speaker:Player    
看起来挺好糊弄的，可以随便做点调查就交差	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_three
    - else:
      ->b_first_two    
    }





=== b_first_three === 
那你准备如何处理这个委托？	#Speaker:NPC 


+ 我准备走访知情人调查线索	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:15  @Speaker:Player    
我准备走访知情人调查线索	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
      ->b_first_three    
    }

+ 我准备去警局询问情况	   @SpeechArt:Pursuade  @CanUse:(0,5,0,0)    @Success:5  @Fail:15  @Speaker:Player    
我准备去警局询问情况	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
      ->b_first_three    
    }

+ 我大概猜到他是谁了	   @SpeechArt:Cheat  @CanUse:(0,3,0,0)    @Success:5  @Fail:15  @Speaker:Player    
我大概猜到他是谁了	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
      ->E_three    
    }





=== b_two === 
您能帮我找出他吗？	#Speaker:NPC 


+ 请放心，女士，一定行的。	   @SpeechArt:Threaten  @CanUse:(0,-5,0,0)    @Success:5  @Fail:10  @Speaker:Player    
请放心，女士，一定行的。	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_one
    - else:
      ->b_two    
    }

+ 我会不惜一切手段把他逮出来。	   @SpeechArt:Threaten  @CanUse:(0,-5,0,0)    @Success:5  @Fail:10  @Speaker:Player    
我会不惜一切手段把他逮出来。	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_one
    - else:
      ->b_two    
    }

+ 没有人是我找不出来的。	   @SpeechArt:Threaten  @CanUse:(0,0,0,-5)    @Success:5  @Fail:10  @Speaker:Player    
没有人是我找不出来的。	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_one
    - else:
      ->b_two    
    }





=== b_two_one === 
但我担心他能逃脱惩罚	#Speaker:NPC 


+ 我会用自己的方法来制裁他	   @SpeechArt:Threaten  @CanUse:(0,-5,0,0)    @Success:5  @Fail:15  @Speaker:Player    
我会用自己的方法来制裁他	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_two
    - else:
      ->b_two_one    
    }

+ 他逃得过警察的惩罚，逃不过我的制裁	   @SpeechArt:Threaten  @CanUse:(0,-5,0,-5)    @Success:5  @Fail:15  @Speaker:Player    
他逃得过警察的惩罚，逃不过我的制裁	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_two
    - else:
      ->b_two_one    
    }





=== b_two_two === 
你做到能帮我找出他吗？	#Speaker:NPC 


+ 应该没问题吧	   @SpeechArt:Threaten  @CanUse:(0,-4,0,-4)    @Success:5  @Fail:5  @Speaker:Player    
应该没问题吧	#Speaker:Player 
{judgeSuccess==true:
    ->E_two
    - else:
      ->b_two_two    
    }

+ 关于他的身份我已经有想法了	   @SpeechArt:Cheat  @CanUse:(0,0,0,-6)    @Success:5  @Speaker:Player    
关于他的身份我已经有想法了	#Speaker:Player 
{judgeSuccess==true:
    ->E_two
    - else:
      ->E_three    
    }





=== E_one === 
接到你的保证后，她将自己所知道的信息如实相告，并且嘱咐你保守好她的信息，随后将定金留在桌子上，接着就离开了。根据她提供的信息，你决定过两天去下城区实地调查一下。	#Speaker:NPC 

-> END





=== E_two === 
接到你的保证后，她将自己所知道的信息如实相告，并且嘱咐你保守好她的信息，随后将定金留在桌子上，接着就离开了。你觉得这调查很难有突破，决定随便收集一下线索就糊弄过去。所以去下城区调查的事不急，先休息比较重要。	#Speaker:NPC 

-> END





=== E_three === 
她察觉到你在敷衍她，非常气愤，双眼怒视着你，说道：“人命关天的时候你都这么敷衍，你是不是跟他们一伙的阿？”	#Speaker:NPC 


+ 即时补救，承认自己刚刚走神了，请求她把信息再说一遍	   @SpeechArt:Normal  @Speaker:Player    
	#Speaker:Player 
	-> b_two_two

+ 无所谓，觉得这个委托太过麻烦，不接手也罢	   @SpeechArt:Normal  @Speaker:Player    
	#Speaker:Player 
	-> END

