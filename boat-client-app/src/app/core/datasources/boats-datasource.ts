import { DataSource } from '@angular/cdk/collections';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { map } from 'rxjs/operators';
import { Observable, merge, BehaviorSubject } from 'rxjs';
import { Injectable } from '@angular/core';
import { BoatDto } from '../models/boat.model';
import { BoatService } from '../services/boat.service';

/**
 * Data source for the Boats view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
@Injectable({
  providedIn: 'root'
})
export class BoatsDataSource extends DataSource<BoatDto> {
  paginator: MatPaginator | undefined;
  sort: MatSort | undefined;
  dataLoaded$ = new BehaviorSubject<BoatDto[]>([]);

  constructor(private boatService: BoatService) {
    super();
    this.loadBoats();
  }

  loadBoats(): void {
    this.boatService.getAll().subscribe((boats) => {
      this.dataLoaded$.next(boats);
    });
  }

  addBoat(newBoat: BoatDto): void {
    const currentBoats = this.dataLoaded$.getValue();
    const updatedBoats = [...currentBoats, newBoat];
    this.dataLoaded$.next(updatedBoats);
  }

  updateBoat(updatedBoat: BoatDto): void {
    const currentBoats = this.dataLoaded$.getValue();
    const updatedBoats = currentBoats.map(boat =>
      boat.id === updatedBoat.id ? updatedBoat : boat
    );
    this.dataLoaded$.next(updatedBoats);
  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<BoatDto[]> {
    if (this.paginator && this.sort) {
      // Combine everything that affects the rendered data into one update
      // stream for the data-table to consume.
      return merge(this.dataLoaded$, this.paginator.page, this.sort.sortChange)
        .pipe(map(() => {
          return this.getPagedData(this.getSortedData([...this.dataLoaded$.value ]));
        }));
    } else {
      throw Error('Please set the paginator and sort on the data source before connecting.');
    }
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect(): void {}

  /**
   * Paginate the data (client-side). If you're using server-side pagination,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getPagedData(data: BoatDto[]): BoatDto[] {
    if (this.paginator) {
      const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
      return data.splice(startIndex, this.paginator.pageSize);
    } else {
      return data;
    }
  }

  /**
   * Sort the data (client-side). If you're using server-side sorting,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getSortedData(data: BoatDto[]): BoatDto[] {
    if (!this.sort || !this.sort.active || this.sort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      const isAsc = this.sort?.direction === 'asc';
      switch (this.sort?.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'desc': return compare(a.description, b.description, isAsc);
        case 'id': return compare(a.id, b.id, isAsc);
        default: return 0;
      }
    });
  }
}

/** Simple sort comparator for example ID/Name columns (for client-side sorting). */
function compare(a: string | number, b: string | number, isAsc: boolean): number {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
