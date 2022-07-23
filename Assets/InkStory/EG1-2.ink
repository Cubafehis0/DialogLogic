->task_start
INCLUDE JudgeHeader.ink



=== task_start === 
平民区某住宅里…	#Speaker:Dialogue 


你轻轻叩响了房门，里面无人应答。你加大了力度，再一次的敲响了房门，依然如故，住宅里仍然平静。除了你与房门的敲击声，门外还能听见废弃装置齿轮转动的“咔咔”声。	#Speaker:Dialogue 


一开始，你以为自己只是恰巧在主人出门的时候来了，转身想走，但是一个角落里的东西吸引了你的注意力。这是一个垃圾桶，里面装着许多速食食品的包装，你站在那里思索了一下。	#Speaker:Dialogue 
-> b_zero



=== b_zero === 
（心理）这里是否有隐藏的信息？	#Speaker:NPC 


+ 有，速食食品包装堆积说明住户已经很久没出门了，他肯定在里面	   @SpeechArt:Pursuade  @Speaker:Player    
	#Speaker:Player 
-> b_zero_one

+ 没有，这就是普通的住户垃圾罢了，我下次再来吧	   @SpeechArt:Threaten  @Speaker:Player    
	#Speaker:Player 
-> END




=== b_zero_one === 
你决定闯进住宅，但是你在考虑如何进入	#Speaker:Dialogue 


+ 用身体将门撞开	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,-1,-1)  
	#Speaker:Player 
-> narrator_one

+ 加大力度再敲几次门	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
	#Speaker:Player 
-> narrator_two




=== narrator_one === 
一段时间后，一个面容憔悴，衣衫杂乱的中年男子打开门，探出头来询问：“是谁啊？”	#Speaker:Dialogue 
-> start



=== narrator_two === 
你撞开房门，发现一个面容憔悴，衣衫杂乱的中年男子窝在房间里。他看见你，惊恐地询问你：“你怎么进来的？你是谁？”	#Speaker:Dialogue 

-> start




=== start === 
+ 我是来收保护费的	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
我是来收保护费的	#Speaker:Player 
-> b_first

+ 把保护费交出来；	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,-1,-1)  
把保护费交出来；	#Speaker:Player 
-> b_first

+ 这个月的保护费你能给我吗？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,1,1,-1)  
这个月的保护费你能给我吗？	#Speaker:Player 
-> b_first

+ 退出选项	   @SpeechArt:Normal  @Speaker:Player    
退出	#Speaker:Player 
-> END





=== b_first === 
我可没有钱给你	#Speaker:NPC 


+ 你这有没有值钱的东西	   @SpeechArt:Normal  @Speaker:Player    
你这有没有值钱的东西	#Speaker:Player 
-> b_frist_one

+ 你受到了我们的保护，应该交保护费	   @SpeechArt:Pursuade  @CanUse:(0,3,0,3)    @Success:5  @Fail:5  @Speaker:Player    
你受到了我们的保护，应该交保护费	#Speaker:Player 
{judgeSuccess==true:
    ->b_three
    - else:
    ->b_frist_one    
}

+ 你怎么可能没钱？	   @SpeechArt:Threaten  @CanUse:(0,-3,0,2)    @Success:5  @Fail:5  @Speaker:Player    
你怎么可能没钱？	#Speaker:Player 
{judgeSuccess==true:
    ->b_two
    - else:
    ->b_first    
}

+ 我知道你把值钱的东西藏在那里	   @SpeechArt:Cheat  @CanUse:(0,3,0,5)    @Success:5  @Fail:5  @Speaker:Player    @StateChange:(0,0,0,1,-1)  
我知道你把值钱的东西藏在那里	#Speaker:Player 
{judgeSuccess==true:
    ->b_three
    - else:
    ->b_frist_one    
}





=== b_frist_one === 
他低下头，转过身去，说：“你看，真的没有值钱的东西。”	#Speaker:NPC 


+ 因为我们你才能过的安稳，当然要交钱给我们。	   @SpeechArt:Pursuade  @CanUse:(0,3,3,0)    @Success:5  @Fail:5  @Speaker:Player    
	#Speaker:Player 
{judgeSuccess==true:
    ->b_three
    - else:
    ->b_first_two    
}

+ 那你什么时候能有钱给我？	   @SpeechArt:Pursuade  @CanUse:(0,2,0,2)    @Success:5  @Fail:5  @Speaker:Player    
那你什么时候能有钱给我？	#Speaker:Player 
{judgeSuccess==true:
    ->b_two
    - else:
    ->b_first_two    
}

+ 如果没钱你怎么能在这住？	   @SpeechArt:Threaten  @CanUse:(0,0,-3,-3)    @Success:5  @Fail:10  @Speaker:Player    
如果没钱你怎么能在这住？	#Speaker:Player 
{judgeSuccess==true:
    ->b_two
    - else:
    ->b_first_two    
}





=== b_first_two === 
他抬起头，双眼直直地瞪着你：“你究竟想做什么？”	#Speaker:NPC #StateChange:(0,0,0,1,-1)  


+ 我就是来收保护费的	   @SpeechArt:Pursuade  @CanUse:(0,3,3,0)    @Success:5  @Fail:5  @Speaker:Player    
我就是来收保护费的	#Speaker:Player 
{judgeSuccess==true:
    ->b_three
    - else:
    ->b_first_two    
}

+ 没什么，只是来看看你的	   @SpeechArt:Pursuade  @CanUse:(0,0,3,3)    @Success:5  @Fail:5  @Speaker:Player    
没什么，只是来看看你的	#Speaker:Player 
{judgeSuccess==true:
    ->b_three
    - else:
    ->b_first_two    
}

+ 少废话，把钱交出来	   @SpeechArt:Threaten  @CanUse:(0,2,2,2)    @Success:5  @Fail:10  @Speaker:Player    
少废话，把钱交出来	#Speaker:Player 
{judgeSuccess==true:
    ->b_two
    - else:
    ->b_first_two    
}





=== b_two === 
“要钱没有，要命一条！”他涨红了脸。	#Speaker:NPC 


+ 别激动，我们就是收点钱而已	   @SpeechArt:Pursuade  @CanUse:(0,2,0,-2)    @Success:5  @Fail:5  @Speaker:Player    @StateChange:(0,0,1,0,-1)  
别激动，我们就是收点钱而已	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
    ->b_two    
}

+ 你没钱，你父母亲人总有吧。	   @SpeechArt:Threaten  @CanUse:(0,-3,-3,-3)    @Success:5  @Fail:10  @Speaker:Player    
你没钱，你父母亲人总有吧。	#Speaker:Player 
{judgeSuccess==true:
    ->b_two_one
    - else:
    ->b_two    
}





=== b_two_one === 
他沉默了一会，说：“别找其他人的麻烦，找我就行了。”	#Speaker:NPC #StateChange:(0,0,-5,0,-1)  


+ 那你给我钱就解决了	   @SpeechArt:Pursuade  @CanUse:(0,2,0,-2)    @Success:5  @Fail:5  @Speaker:Player    
那你给我钱就解决了	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
    ->b_two_one    
}

+ 那你还有什么好说的？赶快把钱给我。	   @SpeechArt:Threaten  @CanUse:(0,-3,-8,-3)    @Success:5  @Fail:15  @Speaker:Player    
那你还有什么好说的？赶快把钱给我。	#Speaker:Player 
{judgeSuccess==true:
    ->E_three
    - else:
    ->b_two_one    
}





=== b_three === 
他看了看手中空空如也的钱包，哀求道：“能不能再给我宽限几天，我一定按时给钱。”	#Speaker:NPC 


+ 我给你想想办法，你可以去我们赌场做事抵钱，我们正缺人手呢	   @SpeechArt:Pursuade  @CanUse:(0,4,2,0)    @Success:5  @Fail:5  @Speaker:Player    
我给你想想办法，你可以去我们赌场做事抵钱，我们正缺人手呢	#Speaker:Player 
{judgeSuccess==true:
    ->b_three_one
    - else:
    ->b_three    
}

+ 我也是被别人派来的，你这样我很难办啊	   @SpeechArt:Pursuade  @CanUse:(0,2,4,0)    @Success:5  @Fail:5  @Speaker:Player    
我也是被别人派来的，你这样我很难办啊	#Speaker:Player 
{judgeSuccess==true:
    ->b_three_one
    - else:
    ->b_three    
}

+ 你没钱的话就去找亲人朋友借啊，他们总有钱的吧	   @SpeechArt:Threaten  @CanUse:(0,-2,0,2)    @Success:5  @Fail:10  @Speaker:Player    
你没钱的话就去找亲人朋友借啊，他们总有钱的吧	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
    ->b_three    
}





=== b_three_one === 
他抓着你的裤脚，恳求你再给他延长期限	#Speaker:NPC 


+ 可以，但是每延迟一天都要加利息	   @SpeechArt:Pursuade  @Speaker:Player    
可以，但是每延迟一天都要加利息	#Speaker:Player 
-> E_two

+ 你已经拖了很久了，宽限这件事我觉得不行	   @SpeechArt:Pursuade  @CanUse:(0,0,8,0)    @Success:5  @Fail:5  @Speaker:Player    
你已经拖了很久了，宽限这件事我觉得不行	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
    ->b_three_one    
}

+ 你已经拖得够久了吧，你是想赖账吗？	   @SpeechArt:Threaten  @CanUse:(0,-2,0,2)    @Success:5  @Fail:15  @Speaker:Player    
你已经拖得够久了吧，你是想赖账吗？	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_two
    - else:
    ->b_three_one    
}





=== E_one === 
他翻遍了自己身上所有的口袋也只找出了孤零零的几枚硬币，看着你面无表情的脸色，他无奈地叹了口气，只好将房契抵押在你那里，等之后再赎回去。	#Speaker:NPC 

-> END





=== E_two === 
你看着跪在地上哀求你的他，仿佛看到了自己小时候的父亲，心中有些于心不忍，于是弯腰将他扶起来，说“我可以宽限你几天，但是几天后我必须要拿到钱，不然后果自负。”	#Speaker:NPC 

-> END





=== E_three === 
他跪在地上的哀求没有打动你的铁石心肠，你决定不拿到钱绝不善罢甘休。你在屋里巡视了一圈，将屋里翻得天翻地覆，拿走了所有可以换钱的东西，之后一脚踢开房门，扬长而去，只留下缩在角落的他和一片狼藉的客厅	#Speaker:NPC 

-> END














