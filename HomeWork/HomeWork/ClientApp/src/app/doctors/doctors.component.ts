import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Doctors } from '../Models/doctors.model';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
  DoctorsList: Array<Doctors>;
  doctor = new Doctors();

  constructor(private http: HttpClient) {
    this.ShowAll();
  }

  ngOnInit() {
  }
  ShowAll() {
    this.http.get<Array<Doctors>>("https://localhost:44329/doctors/getall").subscribe(x => this.DoctorsList = x);
  }
  AddDoctor() {
    this.http.post("https://localhost:44329/doctors/add", this.doctor).subscribe(x => this.ShowAll());
    this.ShowAll();
  }
  Update(id: number, firstName: string, lastName: string, academicTitle: string, email: string, phoneNumber: number, specialization: string) {
    var edit = new Doctors()
    {
      edit.Id = id,
        edit.FirstName = firstName,
        edit.LastName = lastName,
        edit.AcademicTitle = academicTitle,
        edit.Email = email,
        edit.PhoneNumber = phoneNumber,
        edit.Specialization = specialization
    };
    this.http.put("https://localhost:44329/doctors/edit", edit).subscribe(x => this.ShowAll());
  }
  Remove(id: number) {
    this.http.delete("https://localhost:44329/doctors/" + id).subscribe(x => this.ShowAll());
  }
  Filter(name: string) {
    this.http.get<Array<Doctors>>("https://localhost:44329/doctors/filter/" + name).subscribe(x => this.DoctorsList = x);
  }
}
