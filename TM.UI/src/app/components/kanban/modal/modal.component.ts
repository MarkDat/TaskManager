import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee.class';
import { EmployeeService } from '../../../services/employee.service';

@Component({
  selector: 'app-modal',
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent implements OnInit {
  employees: Employee[];
  showCheckBoxesModes: string[] = ["normal", "selectAll", "none"];
  showCheckBoxesMode: string = this.showCheckBoxesModes[0];

  constructor(
    private employeeService : EmployeeService
  ) { }

  ngOnInit(): void {
    this.employees = this.employeeService.getEmployees();
  }

}
