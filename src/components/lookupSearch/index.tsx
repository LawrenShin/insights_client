import React, {useEffect, useState} from 'react';
import useStyles from "./useStyles";
import {CircularProgress, ListItem, ListItemText, TextField} from "@material-ui/core";
import {Squared} from "../button";
import {connect} from "react-redux";
import {RootState} from "../../store/rootReducer";
import {Dispatch} from "redux";
import {CreateAction} from "../../store/actionType";
import {LookupSearchActionType, PaginationActionTypes, State as StateProps} from "./duck";
import {FixedSizeList, ListChildComponentProps} from 'react-window';
import {usePrevious} from "../../helpers";
import {RequestStatuses} from "../../api/requestTypes";
import {useHistory} from 'react-router-dom';
import {ResultsActionType} from '../../pages/results/duck';

interface OwnProps {
  tab: string;
  saveTab: (tab: string) => void;
}
interface DispatchProps {
  lookupRequest: (url: string, params: string) => void;
  resultsRequest: (url: string, params: string) => void;
  incrementPageNumber: () => void;
  clearSearch: () => void;
  saveSearch: (search: string) => void;
}
interface Props extends StateProps, DispatchProps, OwnProps {}
// NOTE: were written with thought of sending request from here on click on show all results button.
// but eventually if we select only options button in here stays disabled. So using this makes sence if we provide name
// besides options for industries. It makes 2 places where we can initiate the request. (here and from indOptions)
const getUrl = (tab: string) => {
  if (tab === 'company') return 'companiesLookup';
  if (tab === 'industry') return 'industries';
  if (tab === 'country') return 'countries';
  return '';
};

const LookupSearch = ({
    incrementPageNumber,
    resultsRequest,
    lookupRequest,
    clearSearch,
    saveSearch,
    data,
    status,
    error,
    tab,
    saveTab,
  }: Props) => {
  const [search, setSearch] = useState<string>('');
  const [timer, setTimer] = useState<any>(null);
  const styles = useStyles();
  const {
    companies,
    options,
    pagination: {
      pageCount,
      pageNumber,
      pageSize,
    },
  } = data;
  const prevSearch = usePrevious(search);
  const prevPageNumber = usePrevious(pageNumber);
  const history = useHistory();
  const isIndustryTab = tab === 'industry';

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
        lookupRequest( getUrl(tab), params );
        // NOTE: not sure what dats for
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
        {
          ['Company Database', 'Industry Database', 'Country database'].map(item => {
            const tabName = item.split(' ')[0].toLowerCase();
            return (<div
              key={item}
              className={tabName === tab ? styles.selected : styles.unselected}
              onClick={() => saveTab(tabName)}
            >
              <span>{item}</span>
            </div>)
          })
        }

      </div>
      <div className={styles.inputContainer}>
        <TextField
          type={'text'}
          size={'small'}
          name={'search'}
          placeholder={'Search...'}
          variant="outlined"
          disabled={tab === 'industry'}
          // error={}
          // helperText={}
          value={search}
          onChange={(e) => setSearch(e.target.value)}
        />
        <Squared
          className={`${styles.button}`}
          disabled={
            (() => {
              if (isIndustryTab && !options.length) return true;
              if (!companies.length && !isIndustryTab) return true;
              return false;
            })()
          }
          type={'button'}
          onClick={() =>
            history.push('/results', {requestParams: isIndustryTab ? options : search})
          }
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
