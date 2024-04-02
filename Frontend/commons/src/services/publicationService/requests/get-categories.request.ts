import { http } from "../../../http";
import { CategoryType } from "../publicationTypes";

export const getCategories = async (
  accessToken: string
): Promise<CategoryType[]> => {
  const result = await http<CategoryType[]>({
    path: "/publication/categories",
    method: "get",
    accessToken,
  });

  if (result.ok && result.body) {
    return result.body;
  } else {
    return [];
  }
};
