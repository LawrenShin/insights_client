import React from 'react';
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import { DataGrid } from '@material-ui/data-grid';
import { useDemoData } from '@material-ui/x-grid-data-generator';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {Dispatch} from "redux";


const Results = () => {
  const styles = useStyles();
  const { data } = useDemoData({
    dataSet: 'Commodity',
    rowLength: 100,
    maxColumns: 6,
  });
  const [page, setPage] = React.useState(0);

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
            page={page}
            onPageChange={(params) => {
              setPage(params.page);
            }}
            pageSize={5}
            autoHeight
            pagination
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
