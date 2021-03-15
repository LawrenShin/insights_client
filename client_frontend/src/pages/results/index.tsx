import React from 'react';
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import {DataGrid} from '@material-ui/data-grid';
import { useDemoData } from '@material-ui/x-grid-data-generator';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {Dispatch} from "redux";
import CustomPagination from "./customPagination";


const Results = () => {
  const styles = useStyles();
  const { data } = useDemoData({
    dataSet: 'Commodity',
    rowLength: 100,
    maxColumns: 6,
  });
  const [page, setPage] = React.useState(0);
  console.log(data)
  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>

        <span>Main search</span>
        <Typography variant={'h5'}>
          List of companies
        </Typography>

        <div className={styles.content}>
          <DataGrid
            className={styles.dataGrid}
            page={page}
            pageSize={5}
            autoHeight
            pagination
            components={{ Pagination: CustomPagination }}
            {...data}
          />
        </div>
      </div>
    </div>
  )
}

const connecter = () => connect(
  (state: RootState) => ({

  }),
  (dispatch: Dispatch) => ({

  })
)

export default connecter()(Results);
