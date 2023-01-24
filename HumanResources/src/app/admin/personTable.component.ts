import { Component, IterableDiffer, IterableDiffers, ViewChild } from "@angular/core";
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatSort } from "@angular/material/sort";
import { Person } from "../persons/person";
import { PersonService } from "../persons/person.service";

@Component({
  styleUrls: ["personTable.component.css"],
  templateUrl: "personTable.component.html"
})
export class PersonTableComponent {
  colsAndRows: string[] = ['id', 'name', 'category', 'birth', 'rating', 'buttons'];
  public persons!: MatTableDataSource<Person>;


  //differ: IterableDiffer<Person>;

  @ViewChild(MatPaginator)
  paginator!: MatPaginator;
  @ViewChild(MatSort)
  sort!: MatSort;

  defaultPageIndex: number = 0;
  defaultPageSize: number = 4;
  public defaultSortColumn: string = "name";
  public defaultSortOrder: "asc" | "desc" = "asc";
  defaultFilterColumn: string = "name";
  filterQuery?: string;

  constructor(private personService: PersonService/*, differs: IterableDiffers*/) {
    
  }

  ngOnInit(): void {
    this.loadData();
  }
  loadData(query?: string) {
    var pageEvent = new PageEvent();
    pageEvent.pageIndex = this.defaultPageIndex;
    pageEvent.pageSize = this.defaultPageSize;
    this.filterQuery = query;
    this.getData(pageEvent);

    // this.differ = differs.find(this.personService.getData()).create();
  }
  getData(event: PageEvent) {
    var sortColumn = (this.sort)
      ? this.sort.active
      : this.defaultSortColumn;
    var sortOrder = (this.sort)
      ? this.sort.direction
      : this.defaultSortOrder;
    var filterColumn = (this.filterQuery)
      ? this.defaultFilterColumn
      : null;
    var filterQuery = (this.filterQuery)
      ? this.filterQuery
      : null;

    this.personService.getData(
      event.pageIndex,
      event.pageSize,
      sortColumn,
      sortOrder,
      filterColumn,
      filterQuery).subscribe(result => {
        this.paginator.length = result.totalCount;
        this.paginator.pageIndex = result.pageIndex;
        this.paginator.pageSize = result.pageSize;
        this.persons = new MatTableDataSource<Person>(result.data);
      }, error => console.error(error));
  }

  deletePerson(id: number) {
    this.personService.delete(id).subscribe(result => {
      console.log("person deleted");
      this.loadData();
    }, error => console.error(error));
  }
}
