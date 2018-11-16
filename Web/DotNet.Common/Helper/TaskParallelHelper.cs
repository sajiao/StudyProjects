using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DotNet.Common
{
    using System.Threading.Tasks;

    public class TaskParallelHelper
    {
       public static void ExecuteParallelTasks(Action[] actions)
       {
           Parallel.Invoke(actions);
       }

       public static void ExecuteTasks(Action[] actions)
       {
           if (actions != null)
           {
               for (int i = 0; i < actions.Count(); i++)
               {
                   Task.Factory.StartNew(actions[i]);
               }
           }
       }

       public static void ExecuteWaitAllTasks(Action[] actions)
       {
           if (actions != null)
           {
               var tasks = new Task[actions.Count()];
               for (int i = 0; i < actions.Count(); i++)
               {
                    tasks[i] = Task.Factory.StartNew(actions[i]);
               }
               Task.WaitAll(tasks);
           }
       }

       public static void ExecuteTask(Action action1)
       {
           if (action1 != null)
           {
               Task.Factory.StartNew(action1);
           }
       }

       public static void ExecuteWaitAllTasks(Action action1, Action action2)
       {
           if (action1 != null && action2 != null)
           {
               
               var actions = new Action[2];
               actions[0] = action1;
               actions[1] = action2;
               ExecuteWaitAllTasks(actions);
           }
       }

       public static void ExecuteWaitAllTasks(Action action1, Action action2, Action action3)
       {
           if (action1 != null && action2 != null && action3 != null)
           {

               var actions = new Action[3];
               actions[0] = action1;
               actions[1] = action2;
               actions[2] = action3;
               ExecuteWaitAllTasks(actions);
           }
       }

       public static void ExecuteWaitAllTasks(Action action1, Action action2, Action action3, params Action[] actionList)
       {
           if (action1 != null && action2 != null && action3 != null)
           {
               int actionCount = 3;
               if (actionList != null)
               {
                   actionCount += actionList.Count();
               }
               var actions = new Action[actionCount];
               actions[0] = action1;
               actions[1] = action2;
               actions[2] = action3;
               if (actionList != null)
               {
                   for (int i = 3; i < actionCount; i++)
                   {
                       actions[i] = actionList[i - 3];
                   }
               }
               ExecuteWaitAllTasks(actions);
           }
       }
    }
}
