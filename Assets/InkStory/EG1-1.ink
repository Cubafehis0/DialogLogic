->task_start
INCLUDE JudgeHeader.ink



=== task_start === 
你接到了黑帮头目的要求，去收这个月的保护费。	#Speaker:Dialogue 


平民区的居住者主要是没有身份和大量金钱的人，他们每天忙碌工作却只够糊口。	#Speaker:Dialogue 


这里充斥着暴力和混乱，而这个混乱地区的律令由欧佩克帮和托拉斯家族两个组织进行维持。	#Speaker:Dialogue 


前一个是移动世界中最大的暴力组织，而后一个则是新近崛起、臭名昭著的军火贩子。	#Speaker:Dialogue 




你前往这次收取保护费的地方。	#Speaker:Dialogue 


这是一家穷困潦倒的黑户，向你们缴纳保护费才得以在这里生活下去，但保护费也让他们家徒四壁。	#Speaker:Dialogue 


当你走到一个小院，准备敲响房门收保护费时，此时一个皱着眉头的年轻女子出现在你面前，她看起来有点瑟缩，甚至是有点害怕和你对视，但她打起精神望向你。	#Speaker:Dialogue 


你是来做什么的？	#Speaker:NPC 


+ 我是来收保护费的	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,1,0,0,-1)  
我是来收保护费的	#Speaker:Player 
-> b_first

+ 我是来做应该做的事情的	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,0,-1,-1)  
我是来做应该做的事情的	#Speaker:Player 
-> b_first

+ 请问你是那位？	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,-1,0,-1)  
请问你是那位？	#Speaker:Player 
-> b_two

+ 没有什么事，我这就离开	   @SpeechArt:Normal  @Speaker:Player    
	#Speaker:Player 
	-> END





=== b_first === 
收保护费?	#Speaker:NPC 


女子看看身后显得有些破破烂烂的屋子。	#Speaker:Dialogue 


你和他们是一伙的？	#Speaker:NPC 


+ 我是	   @SpeechArt:Normal  @Speaker:Player    
我是来收保护费的	#Speaker:Player 
-> b_two

+ 我不太清楚你指的什么	   @SpeechArt:Normal  @Speaker:Player    @StateChange:(0,0,0,1,-1)  
我不太清楚你指的什么	#Speaker:Player 
-> b_two

+  我劝你少管闲事 	   @SpeechArt:Threaten  @CanUse:(0,0,0,-3)    @Success:5  @Fail:5  @Speaker:Player    
我劝你少管闲事 	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_one
    - else:
    ->E_two    
  }

+ 我们不是一伙的，我会妥善处理这件事	   @SpeechArt:Cheat  @CanUse:(0,7,0,0)    @Success:5  @Fail:5  @Speaker:Player    
  他可不止欠了一家的钱，我知道你说的谁但我们不是一伙的，我向你保证我会妥善处理这件事		#Speaker:Player 
  {judgeSuccess==true:
    ->b_two
    - else:
      ->b_first_one    
      }





=== b_first_one === 
她往后退了一步，但仍然抬头直视你。	#Speaker:Dialogue 


你到底想干什么？	#Speaker:NPC 


+  [少跟老子来这套] 	   @SpeechArt:Threaten  @CanUse:(0,3,0,-3)    @Success:5  @Fail:10  @Speaker:Player    
	#Speaker:Player 
	你握紧拳头	#Speaker:Dialogue 

少跟老子来这套，干当然是用拳头干。	#Speaker:Player 

{judgeSuccess==true:
    ->E_two

    - else:
        ->E_two    
        }

你握紧拳头	#Speaker:Dialogue 


少跟老子来这套，干当然是用拳头干。	#Speaker:Player 


+ 我只是做该做的而已。	   @SpeechArt:Persuade  @CanUse:(0,0,0,-2)    @Success:5  @Fail:5  @Speaker:Player    
我只是做该做的而已。	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
      ->b_one_two    
      }

+ 我们只是在保护他们免受伤害，我们才是正义的一方。	   @SpeechArt:Cheat  @CanUse:(0,3,2,-3)    @Success:5  @Fail:5  @Speaker:Player    
  我们只是在保护他们免受伤害，我们才是正义的一方。	#Speaker:Player 
  {judgeSuccess==true:
    ->E_one
    - else:
      ->E_two    
      }





=== b_one_two === 
你以为我不知道吗？你根本就只是来勒索，为了满足你们暴徒的愿望来干这些龌蹉事！	#Speaker:NPC #StateChange:(0,-1,-1,1,2)  


+ 闪开，否则我就要动手了	   @SpeechArt:Threaten  @CanUse:(0,0,0,-3)    @Success:5  @Fail:10  @Speaker:Player    
闪开，否则我就要动手了	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
      ->E_two    
      }

+ 他们接受我们的保护，没有我们他们早就因为黑户被管理局抓走了。	   @SpeechArt:persuade  @CanUse:(0,2,0,0)    @Success:5  @Fail:10  @Speaker:Player    
他们接受我们的保护，没有我们他们早就因为黑户被管理局抓走了。	#Speaker:Player 
{judgeSuccess==true:
    ->E_one
    - else:
      ->E_two    
      }

+ 连他的生计都是我们提供的，他活该今天被我们讨债，你少在这里碍事。	   @SpeechArt:Cheat  @CanUse:(0,3,0,-2)    @Success:5  @Fail:10  @Speaker:Player    
  连他的生计都是我们提供的，他活该今天被我们讨债，你少在这里碍事。	#Speaker:Player 
  {judgeSuccess==true:
    ->E_one
    - else:
      ->E_two    
      }





=== b_two === 
我是他们的邻居，我来帮助他们。	#Speaker:NPC 


+ 你并不知道发生了什么，这件事与你无关	   @SpeechArt:Persuade  @CanUse:(0,2,0,-2)    @Success:5  @Fail:10  @Speaker:Player    
你并不知道发生了什么，这件事与你无关	#Speaker:Player 
{judgeSuccess==true:
    ->b_first_one
    - else:
      ->E_two    
      }

+ 我劝你少管闲事，你挡着我的路了	   @SpeechArt:Threaten  @CanUse:(0,0,0,-6)    @Success:5  @Fail:15  @Speaker:Player    
我劝你少管闲事，你挡着我的路了	#Speaker:Player 
{judgeSuccess==true:
    ->b_one_two
    - else:
      ->E_two    
      }

+ 我是来做我应该做的事情	   @SpeechArt:Normal  @Speaker:Player    
我是来做我应该做的事情	#Speaker:Player 
-> b_first





=== E_one === 
然后她走近两步，直视着你的眼睛，像是要从你眼中读出你内心真实所想，问道：	#Speaker:Dialogue 


你说的是真的吗？你能保证吗？	#Speaker:NPC 


+ 我不确定，但我会尽力达成。	   @SpeechArt:Persuade  @CanUse:(0,-3,0,0)    @Success:5  @Fail:5  @Speaker:Player    
我不确定，但我会尽力达成。	#Speaker:Player 
{judgeSuccess==true:
    ->END
    - else:
      ->E_two    
      }

+ 我目前只知道这笔钱是我们应得的，其他的不归我管，但我可以帮你去找其他人问问。	   @SpeechArt:Cheat  @CanUse:(0,3,0,0)    @Success:5  @Fail:5  @Speaker:Player    
  我目前只知道这笔钱是我们应得的，其他的不归我管，但我可以帮你去找其他人问问。	#Speaker:Player 
  {judgeSuccess==true:
    ->END
    - else:
      ->E_two    
      }





=== E_two === 
她愤怒的看着你，张开双手挡住了你面前的路。	#Speaker:Dialogue 


你别过来，再往前一步我就要找警署了。 	#Speaker:NPC 


你耸耸肩，无所谓的摊开手。   	#Speaker:Dialogue 


悉听尊便，但是你没办法阻挡我。 	#Speaker:Player 


你推开她，走到房门前按响门铃，大声道：	#Speaker:Dialogue 


别磨蹭了，快把钱拿出来。   	#Speaker:Player 


一时间周围悄无声息，只有机器运行时齿轮发出的咔咔声。	#Speaker:Dialogue 


再不出来我就进去了！    	#Speaker:Player 


女子上前死死拉住你的手，试图阻止你进入房子，但你只是哼了一声，随手甩开她，就用脚踢开了房门，径直走了进去。	#Speaker:Dialogue 


不久，房内便被你翻了个底朝天，值钱的东西被你洗劫一空，你对到手的东西非常满意。	#Speaker:Dialogue 


女子缩在角落里颤抖，但她看着屋内的一片狼藉，坚定了去警署报警的决心。在你走后，她前往警署报警。	#Speaker:Dialogue 

-> END

