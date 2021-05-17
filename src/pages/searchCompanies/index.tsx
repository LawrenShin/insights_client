import React, {useState} from 'react';
import LookupSearch from "../../components/lookupSearch";
import CustomSearch from "../../components/customSearch";
import Header from "../../components/Header";
import useStyles from "./useStyles";


const SearchCompanies = () => {
  const [tab, setTab] = useState<string>('company');
  const styles = useStyles();

  return (
    <div className={styles.root}>
      <Header />
      <div className={styles.searchWrapper}>
        <div className={styles.searchContainer}>
          <LookupSearch tab={tab} setTab={setTab} />
        </div>
        <div className={styles.searchSettingsContainer}>
          {/* TODO: some day reconsider this cas custom might also be on companies */}
          {tab !== 'company' && <CustomSearch tab={tab} setTab={setTab} />}
        </div>
      </div>
    </div>
  )
}

export default SearchCompanies;
