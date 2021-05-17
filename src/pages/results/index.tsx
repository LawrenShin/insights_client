import React, {useEffect} from 'react';
import Header from "../../components/Header";
import useStyles from "./useStyles";
import {Typography} from "@material-ui/core";
import {DataGrid} from '@material-ui/data-grid';
import {RootState} from "../../store/rootReducer";
import {connect} from "react-redux";
import {Dispatch} from "redux";
import CustomPagination from "./customPagination";
// import {Rounded} from '../../components/button';
// import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import {useHistory} from "react-router-dom";
import {Pagination as PaginationType} from "../../components/lookupSearch/duck";
import prepareForGrid from "./prepareForGrid";
import {CreateAction} from "../../store/actionType";
import {ResultsActionType} from "./duck";
import {RequestStatuses} from "../../api/requestTypes";
import CustomLoadingOverlay from "../../components/LinearCustomOverlay";
import BreadCrumbs from "../../components/breadCrumbs/breadCrumbs";


export interface LocStateType {
  pagination: PaginationType;
  search: string;
}

// const styledButton = () => <Rounded>EXPORT<ExpandMoreIcon /></Rounded>

// TODO: types
const Results = (props: any) => {
  const {
    data, status, error, search,
    resultsRequest,
    setPagination,
    clearStore,
  } = props;
  const history = useHistory();
  const locState = history.location.state as LocStateType;
  // TODO: create types, typeGuards and split logic for companies and industries
  const {pagination, companies} = data;
  const defineSearch = locState?.search || search;

  const styles = useStyles();
  const makeParams = (pagination: PaginationType, search: string): string => {
    const {pageNumber, pageSize} = pagination;
    const paginationParams = `page_number=${pageNumber}&page_size=${pageSize}`;
    return `search_prefix=${search}&${paginationParams}`;
  }

  const readyForGrid = (companies && companies.length) ? prepareForGrid(data, styles, history): {columns: [], rows: []};
  // initial request
  useEffect(() => {
    resultsRequest('companies', makeParams(pagination, defineSearch));
    return clearStore()
  }, []);
  // TODO: resultsRequest needs optimization
  useEffect(() => {
    resultsRequest('companies', makeParams(pagination, defineSearch));
  }, [pagination]);

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>

        <BreadCrumbs crumbs={['mainSearch']} />
        <Typography variant={'h5'} style={{marginTop : '10px'}}>
          List of companies
        </Typography>

        <div className={styles.content}>
          {<DataGrid
            className={styles.dataGrid}
            page={pagination.pageNumber || 0}
            pageSize={pagination.pageSize || 0}
            onPageChange={(params) => {
              setPagination({...pagination, pageNumber: params.page});
            }}
            // onCellHover={(params) => console.log(params.value)}
            columnBuffer={4}
            pagination
            paginationMode="server"
            // otherwise doesn't work needs total amount of rows. Also set to 0 if want 'No rows' overlay
            rowCount={readyForGrid.rows.length ? pagination.pageSize * pagination.pageCount : 0}
            components={{
              Pagination: (props) =>
                <CustomPagination
                  {...props}
                  pagination={pagination}
                  setPagination={setPagination}
                />,
              // Toolbar: styledButton,
              LoadingOverlay: CustomLoadingOverlay,
            }}
            hideFooterSelectedRowCount={true}
            loading={!companies || status === RequestStatuses.loading}
            {...readyForGrid}
          />}
        </div>
      </div>
    </div>
  )
}

const connector = () => connect(
  (state: RootState) => ({
    ...state.Results,
    search: state.LookupSearch.data.search,
  }),
  (dispatch: Dispatch) => ({
    resultsRequest: (url: string, params: string) =>
      dispatch(CreateAction(ResultsActionType.RESULTS_LOAD, {url, params})),
    setPagination: (pagination: PaginationType) =>
      dispatch(CreateAction(ResultsActionType.RESULTS_PAGINATION, pagination)),
    clearStore: () => dispatch(CreateAction(ResultsActionType.RESULTS_CLEAR)),
  })
)

export default connector()(Results);
