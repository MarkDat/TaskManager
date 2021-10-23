import { Component, OnInit } from '@angular/core';
import { TestService } from 'src/app/services/test.service';

@Component({
  selector: 'app-kanban',
  templateUrl: './kanban.component.html',
  styleUrls: ['./kanban.component.css'],
})
export class KanbanComponent implements OnInit {
  lists: any[] = [];
  statuses: string[] = [
    'Not Started',
    'Need Assistance',
    'In Progress',
    'Deferred',
    'Completed',
  ];
  public employees: Object = {};
  constructor(private service: TestService) {}

  ngOnInit(): void {
    const tasks = this.service.getTasks();

    this.service.getEmployees().forEach((employee) => {
      this.employees[employee.ID] = employee.Name;
    });

    this.statuses.forEach((status) => {
      this.lists.push(tasks.filter((task) => task.Task_Status === status));
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

}
