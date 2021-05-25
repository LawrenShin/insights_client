import React from 'react';
import {Grid} from "@material-ui/core";
import {Rounded} from "../../components/button";
import ExpandMoreIcon from "@material-ui/icons/ExpandMore";
import useStyles from "./useStyles";
import {renderSingleProp} from "./helpers";
import {GeneralInfo as GeneralInfoCompany} from "../../components/details/company/generalInfo";


const GeneralInfo = ({data}: any) => {
  const styles = useStyles();

  return (<>
    <Grid item sm={12} className={styles.paintContainer}>
      <Grid direction={'row'} container >
        <Grid item className={styles.companyHeader}>
          <Grid direction={'row'} container>
            <span className={styles.titleFont}> {data?.companyHeader?.name || data?.name} </span>
            {
              Object.keys(data?.companyHeader).map((key) => {
                if (key !== 'name') return renderSingleProp(key, data.companyHeader[key])
              })
            }
          </Grid>
        </Grid>
        <Rounded>EXPORT<ExpandMoreIcon /></Rounded>
      </Grid>
    </Grid>
    <GeneralInfoCompany data={data} styles={styles} />
  </>);
}

export default GeneralInfo;