import React, {useEffect, useState} from 'react';
import useStyles from "./useStyles";
import {CircularProgress, ListItem, ListItemText, TextField} from "@material-ui/core";
import {Squared} from "../button";
import {connect} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {LookupSearchActionType, Pagination as PaginationType, PaginationActionTypes, State as StateProps} from "./duck";
import {FixedSizeList, ListChildComponentProps} from 'react-window';
import {usePrevious} from "../../helpers";
import {RequestStatuses} from "../../api/requestTypes";
import {useHistory} from 'react-router-dom';
import {ResultsActionType} from '../../pages/results/duck';


interface DispatchProps {
  lookupRequest: (url: string, params: string) => void;
  resultsRequest: (url: string, params: string) => void;
  incrementPageNumber: () => void;
  clearSearch: () => void;
  saveSearch: (search: string) => void;
}
interface Props extends StateProps, DispatchProps {}

const LookupSearch = ({
    incrementPageNumber,
    resultsRequest,
    lookupRequest,
    clearSearch,
    saveSearch,
    data,
    status,
    error,
  }: Props) => {
  const [search, setSearch] = useState<string>('');
  const [timer, setTimer] = useState<any>(null);
  const styles = useStyles();
  const {
    companies,
    pagination: {
      pageCount,
      pageNumber,
      pageSize,
    },
  } = data;
  const prevSearch = usePrevious(search);
  const prevPageNumber = usePrevious(pageNumber);
  const history = useHistory();

  function renderRow(props: ListChildComponentProps) {
    const { index, style } = props;
    const forText = <div
      className={styles.result}
      onClick={() => history.push(`/details/${companies[index].id}`)}
    >
      <span className={styles.bold}>{companies[index].name}</span>,
      <span> {companies[index].country}</span>,
      <span> {companies[index].city}</span>
    </div>

    return (
      <ListItem style={style} key={index} className={styles.listElement}>
        <ListItemText primary={forText} />
      </ListItem>
    );
  }

  useEffect(() => {
    if (timer) {
      clearTimeout(timer);
      setTimer(null);
    }
    const searchChange = search !== prevSearch;
    const pageNumberChange = pageNumber !== prevPageNumber;

    const searchPrefix = `search_prefix=${search}`;
    const pageCountParam = `page_count=${pageCount}`;
    const pageNumberParam = `page_number=${pageNumber}`;
    const pageSizeParam = `page_size=${pageSize}`;
    const params = `${searchPrefix}&${pageCountParam}&${pageNumberParam}&${pageSizeParam}`;

    // TODO: for some reason lookupRequest works 1 of 2 times on search change
    if ((searchChange || pageNumberChange) && search) {
      if (searchChange) clearSearch();
      return setTimer(setTimeout(() => {
        lookupRequest(
          'companiesLookup',
          params
        );
        saveSearch(search);
      }, 500));
    }
    if (!search) return clearSearch();

    return () => {
      resultsRequest('companies', params);
    }
  }, [search, pageNumber, companies]);

  return (<>
    <div className={styles.container}>
      <div className={styles.tabs}>
        <div><span>Company Database</span></div>
      </div>
      <div className={styles.inputContainer}>
        <TextField
          type={'text'}
          size={'small'}
          name={'search'}
          placeholder={'Search...'}
          variant="outlined"
          // error={}
          // helperText={}
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Squared
          className={`${styles.button}`}
          disabled={!companies.length}
          type={'button'}
          onClick={() => history.push('/results', {search})}
        >
          {
            (status === RequestStatuses.loading && !companies.length) ?
            <CircularProgress size={20} />
            : 'Show all results'
          }
        </Squared>
      </div>
      {companies.length > 0 ? <div key={companies.length} className={styles.lookupResults}>
        <FixedSizeList
          className={styles.lookupResultsList}
          itemData={companies}
          height={300}
          width={'100%'}
          itemSize={30}
          itemCount={companies.length}
        >
          {renderRow}
        </FixedSizeList>
          {status !== RequestStatuses.loading ? <button
            className={styles.showMore}
            onClick={incrementPageNumber}
          >SHOW MORE</button>
            :
            <CircularProgress style={{ marginLeft: '40px' }} size={30} />
          }
      </div>
      : null}
    </div>
  </>)
}

export default connect(
  ({ LookupSearch }: RootState) => ({ ...LookupSearch }),

  (dispatch: Dispatch) => ({
    saveSearch: (search: string) => dispatch(CreateAction(LookupSearchActionType.SAVE_SEARCH, search)),
    lookupRequest: (url: string, params: string) =>
      dispatch(CreateAction(LookupSearchActionType.LOOKUP_LOAD, {url, params})),
    resultsRequest: (url: string, params: string) =>
      dispatch(CreateAction(ResultsActionType.RESULTS_LOAD, {url, params})),
    clearSearch: () => dispatch(CreateAction(LookupSearchActionType.LOOKUP_LOAD_CLEAR)),
    incrementPageNumber: () => dispatch(CreateAction(PaginationActionTypes.INCREMENT)),
  })
)(LookupSearch);
