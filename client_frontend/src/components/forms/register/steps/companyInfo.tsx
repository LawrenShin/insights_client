import React from 'react';
import {Grid, Typography} from "@material-ui/core";
import Input from "../../../formFields/input";
import {FormFieldType} from "../formModel";
import useStyles from "./useStyles";
import MySelect from "../../../formFields/select";

interface Props {
  formField: FormFieldType;
}

const CompanyInfo = (props: Props) => {
  const {
    formField: {
      companyName,
      industry,
      country,
    }
  } = props;
  const styles = useStyles();

  return (
    <>
      <Typography className={styles.title} variant="h5" gutterBottom>
        Company information
      </Typography>
      <Grid container spacing={3}>
        <Grid item sm={12}>
          <Input
            className={styles.fullWidth}
            name={companyName.name}
            label={companyName.label}
            variant="outlined"
            size={'small'}
          />
        </Grid>
        <Grid item sm={12}>
          <Input
            className={styles.fullWidth}
            name={industry.name}
            label={industry.label}
            variant="outlined"
            size={'small'}
          />
        </Grid>
        <Grid item sm={12}>
          <MySelect
            className={styles.fullWidth}
            name={country.name}
            label={country.label}
            size={'small'}
          />
        </Grid>
      </Grid>
    </>
  )
}

export default CompanyInfo;
