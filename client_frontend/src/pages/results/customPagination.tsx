import {GridBaseComponentProps} from "@material-ui/data-grid";
import React from "react";
import {FormControl, MenuItem, Select, TextField} from "@material-ui/core";
import Pagination from "@material-ui/lab/Pagination";
import PaginationItem from "@material-ui/lab/PaginationItem";
import useStyles from "./useStyles";
import {Pagination as PaginationType} from "../../components/lookupSearch/duck";


interface Props {
  setPagination: (value: PaginationType) => void;
  pagination: PaginationType;
}

function CustomPagination(props: GridBaseComponentProps & Props) {
  const styles = useStyles();
  const {
    state,
    api,
    pagination,
    setPagination,
  } = props;

  if (!pagination) return null;

  return (<div className={styles.rootPagination}>
    <label htmlFor={'rowsPerPage'}>Show:&nbsp;</label>
    <FormControl
      size={'small'}
    >
      <Select
        className={styles.rowsPerPageSelect}
        name="rowsPerPage"
        value={pagination.pageSize}
        onChange={(e) => {
          const value = e.target.value as number;
          setPagination({
            ...pagination,
            pageNumber: 1,
            pageSize: value,
            // TODO: pageCount will work proper after actual request and valid data
            // pageCount: Math.ceil(),
          })
        }}
        variant="outlined"
      >
        <MenuItem className={styles.menuItem} value={10}>10</MenuItem>
        <MenuItem value={20}>20</MenuItem>
        <MenuItem value={30}>30</MenuItem>
      </Select>
    </FormControl>

    <Pagination
      // className={styles.paginationField}
      variant="outlined"
      shape="rounded"
      page={pagination.pageNumber}
      count={pagination.pageCount}
      // renderItem={(props2) => <PaginationItem {...props2} disableRipple />}
      onChange={(event, value) => api.current.setPage(value)}
    />

    <label htmlFor={'moveToPage'}>Move to:&nbsp;</label>
    {/* TODO: make go to page smoothier */}
    <TextField
      className={styles.jumpToInput}
      type={'text'}
      size={'small'}
      name={'moveToPage'}
      variant="outlined"
      // error={}
      // helperText={}
      value={pagination.pageNumber}
      onChange={(e) => {
        // const value: number = +e.target.value > pagination.pageCount ? pagination.pageCount : +e.target.value;
        setPagination({
          ...pagination,
          pageNumber: +e.target.value || 1,
        });
      }}
    />
  </div>);
}

export default CustomPagination;
