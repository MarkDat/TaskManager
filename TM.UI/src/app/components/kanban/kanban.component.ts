import { Component, OnInit } from '@angular/core';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-kanban',
  templateUrl: './kanban.component.html',
  styleUrls: ['./kanban.component.css'],
})

export class KanbanComponent implements OnInit {
  lists: any[] = [];
  statuses: object[] = [
    {
      title:'Co hoi',
      color:'#8ccbbe'
    },
    {
      title:'Bao gia',
      color:'#c56183'
    },
    {
      title:'Don hang',
      color:'#008891'
    },
    {
      title:'Hoan thanh',
      color:'#4cb043'
    },
    {
      title:'Huy',
      color:'red'
    }
  ];
  
  public employees: Object = {};
  constructor(private service: TestService) {}

  ngOnInit(): void {
    const tasks = this.service.getTasks();

    this.service.getEmployees().forEach((employee) => {
      this.employees[employee.ID] = employee.Name;
    });

    this.statuses.forEach((status) => {
      this.lists.push(tasks.filter((task) => task.Task_Status === status['title']));
    });
  }

  onListReorder(e: any) {
    const list = this.lists.splice(e.fromIndex, 1)[0];
    this.lists.splice(e.toIndex, 0, list);

    const status = this.statuses.splice(e.fromIndex, 1)[0];
    this.statuses.splice(e.toIndex, 0, status);
    console.log("Reorder");
  }

  onTaskDragStart(e: any) {
    e.itemData = e.fromData[e.fromIndex];
    console.log("Start");
  }

  onTaskDrop(e: any) {
    e.fromData.splice(e.fromIndex, 1);
    e.toData.splice(e.toIndex, 0, e.itemData);
    console.log("Drop ");
    console.log(e);
  }

  onCardClick(card : any){
    console.log("Click card");
    console.log(card);
  }
  
  onClickAddNewCard(){
    console.log("Click add new");
  }

  onClickPriority(){
    console.log("onClickPriority()");
  }
}
